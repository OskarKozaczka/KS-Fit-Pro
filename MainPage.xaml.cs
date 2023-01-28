namespace KS_Fit_Pro;

public partial class MainPage : ContentPage
{
    BeltController _beltController;


    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageVM();
        _beltController = new BeltController();
    }

    private async void OnConnectClicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            var status = await Permissions.RequestAsync<BlePermision>();
        }


        var result = await _beltController._connector.AutoConnect();
        if (result) (sender as Button).BackgroundColor = Color.Parse("green");
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

