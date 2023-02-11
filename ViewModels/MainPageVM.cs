using KS_Fit_Pro.Models;
using KS_Fit_Pro.Source;
using Microsoft.Maui.ApplicationModel;
using Plugin.BLE.Abstractions.EventArgs;
using CommunityToolkit.Mvvm.ComponentModel;

namespace KS_Fit_Pro.ViewModels
{
    public partial class MainPageVM : ObservableObject
    {
        [ObservableProperty]
        public int beltSpeed;
        [ObservableProperty]
        public TimeSpan activityTime;
        [ObservableProperty]
        public int activityDistance;
        [ObservableProperty]
        public int steps;
        [ObservableProperty]
        public int calories;
        [ObservableProperty]
        public float sliderSpeedValue;
        [ObservableProperty]
        public bool autoMode;
        [ObservableProperty]
        public bool isConnecting;
        [ObservableProperty]
        public bool isRunning;

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
            AutoMode = !_beltController.CurrentBeltState.IsManualMode;
            Calories = _calCalc.CalculateCalories(_beltController.CurrentBeltState);
        }

        internal void OnSliderValueChanged(double speed)
        {
            var last = 0;
            var current = Interlocked.Increment(ref last);
            Task.Delay(1000).ContinueWith(async task =>
            {
                if (current == last) await _beltController.SetSpeed((int)speed*10);
                task.Dispose();
            });
        }

        internal async Task OnStartClicked()
        {
             await _beltController.Start();
            IsRunning = true;
        }

        internal async Task OnPauseClicked()
        {
            await _beltController.SetSpeed(0);
            _beltController.SaveLastState();
            IsRunning = false;
        }

        internal async Task OnStopClicked()
        {
            await _beltController.SetSpeed(0);
            IsRunning = false;

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
