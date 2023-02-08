using KS_Fit_Pro.Models;
using KS_Fit_Pro.Source;
using Microsoft.Maui.ApplicationModel;
using Plugin.BLE.Abstractions.EventArgs;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace KS_Fit_Pro.ViewModels
{
    public class MainPageVM
    {
        public int BeltSpeed { get; set; }
        public TimeSpan ActivityTime { get; set; }
        public int ActivityDistance { get; set; }
        public int Steps { get; set; }
        public int Calories { get; set; }
        public int SliderSpeedValue { get; set; }
        public bool AutoMode { get; set; }
        public bool IsConnecting { get; set; }

        private readonly BeltController _beltController;
        private readonly ActivityRecordsPageVM _activitiesVM;
        private readonly CaloriesCalculator _calCalc;
        private readonly BLEConnector _ble;
        public MainPageVM(BeltController beltController, ActivityRecordsPageVM activitiesVM, CaloriesCalculator calCalc, BLEConnector ble)
        {
            _beltController = beltController;
            _activitiesVM = activitiesVM;
            _calCalc = calCalc;
            _ble = ble;
            _ble.OnMessageReceived += UpdateValues;
            _ble.OnDeviceConnected += OnConnected;
             IsConnecting = true;
        }

        private void OnConnected(object sender, DeviceEventArgs e)
        {
            IsConnecting = false;
        }
        private void UpdateValues(object sender, EventArgs e)
        {
            BeltSpeed= _beltController.CurrentBeltState.BeltSpeed;
            Steps = _beltController.CurrentBeltState.Steps;
            ActivityDistance = _beltController.CurrentBeltState.ActivityDistance;
            ActivityTime = _beltController.CurrentBeltState.ActivityTime;
            AutoMode = !_beltController.CurrentBeltState.ManualMode;
            Calories = _calCalc.CalculateCalories(_beltController.CurrentBeltState);
        }

        internal void OnSliderValueChanged(int speed)
        {
            var last = 0;
            var current = Interlocked.Increment(ref last);
            Task.Delay(1000).ContinueWith(async task =>
            {
                if (current == last) await _beltController.SetSpeed(speed);
                task.Dispose();
            });
        }

        internal async Task OnStartClicked()
        {
             await _beltController.Start();
        }

        internal async Task OnPauseClicked()
        {
            await _beltController.SetSpeed(0);
            _beltController.SaveLastState();
        }

        internal async Task OnStopClicked()
        {
            await _beltController.SetSpeed(0);

            SaveRecord();
            _beltController.ResetState();
        }

        private void SaveRecord()
        {
            if (ActivityTime < new TimeSpan(0, 0, 5)) return;

            var activity = new Activity()
            {
                TotalDistance = ActivityDistance,
                ActivityDate = DateTime.Now,
                TotalCalories = Calories,
                TotalTime = ActivityTime,
                TotalSteps = Steps,
                AverageSpeed = _beltController.CurrentBeltState.AvgSpeed,
                SpeedHistory = _beltController.CurrentBeltState.SpeedHistory
            };

            _activitiesVM.ActivityRecords.Add(activity);
        }

        internal async void OnToggled(bool value)
        {
            if(value) await _beltController.SetMode(BeltMode.AUTO);
            else await _beltController.SetMode(BeltMode.MANUAL);


        }
    }
}
