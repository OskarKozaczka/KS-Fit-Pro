#if ANDROID
using BLE.Client.Droid.Helpers;
using KS_Fit_Pro.Source;
#endif
using KS_Fit_Pro.Source;

namespace KS_Fit_Pro.Pages;

public partial class MainPage : ContentPage
{
    BeltController _beltController;

    //public MainPage()
    //{

    //}
    public MainPage(MainPageVM vm, BeltController beltController)
    {
        InitializeComponent();
        BindingContext = vm;
        _beltController = beltController;
        
    }

    private async void OnConnectClicked(object sender, EventArgs e)
    {
#if ANDROID
        await Permissions.RequestAsync<BlePermision>();
#endif
        // result = await _beltController._bleConnector.AutoScan();
        //if (result) (sender as Button).BackgroundColor = Color.Parse("green");
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        var last = 0;
        var current = Interlocked.Increment(ref last);
        Task.Delay(1000).ContinueWith(task =>
        {
            if (current == last) _beltController.SetSpeed((int)e.NewValue);
            task.Dispose();
        });

    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await _beltController.Start();
    }
}

