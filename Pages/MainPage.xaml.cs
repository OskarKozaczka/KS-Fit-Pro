using KS_Fit_Pro.Source;
using KS_Fit_Pro.ViewModels;

namespace KS_Fit_Pro.Pages;

public partial class MainPage : ContentPage
{
    MainPageVM vm;
    public MainPage(MainPageVM ViewModel)
    {
        InitializeComponent();
        vm = ViewModel;
        BindingContext = vm;
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        vm.OnSliderValueChanged((int)e.NewValue);
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await vm.OnStartClicked();
    }

    private async void OnStopClicked(object sender, EventArgs e)
    {
        await vm.OnStopClicked();
    }

    private async void OnPauseClicked(object sender, EventArgs e)
    {
        await vm.OnPauseClicked();
    }

    private void OnToggled(object sender, ToggledEventArgs e)
    {
        vm.OnToggled(e.Value);
    }
}

