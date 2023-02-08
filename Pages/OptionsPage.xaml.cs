using KS_Fit_Pro.ViewModels;

namespace KS_Fit_Pro.Pages;

public partial class OptionsPage : ContentPage
{
    OptionsPageVM vm;
    public OptionsPage(OptionsPageVM ViewModel)
    {
        InitializeComponent();
        vm = ViewModel;
        BindingContext = vm;
        WeightEntry.Completed += vm.Save;
    }
    void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        vm.SwitchMode(e.Value);
    }
}