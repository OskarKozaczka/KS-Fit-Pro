using KS_Fit_Pro.Source;
using KS_Fit_Pro.ViewModels;
using Microsoft.Maui.Controls;
using System.IO;

namespace KS_Fit_Pro.Pages;

public partial class MainPage : ContentPage
{
    MainPageVM vm;
    public MainPage(MainPageVM ViewModel)
    {
        InitializeComponent();
        vm = ViewModel;
        BindingContext = vm;

        SetupActivityStats();
    }

    private void SetupActivityStats()
    {
        ActivityData.Add(StatTabFactory($"{vm.ActivityTime}", "Time", 48, 16));

        var grid = new Grid();
        grid.RowDefinitions = new RowDefinitionCollection() { new RowDefinition(), new RowDefinition() };
        grid.ColumnDefinitions = new ColumnDefinitionCollection() { new ColumnDefinition(), new ColumnDefinition() };
        grid.Add(StatTabFactory($"{vm.ActivityDistance} m", "Distance"),0,0);
        grid.Add(StatTabFactory($"{vm.BeltSpeed} km/h", "Speed"),1,0);
        grid.Add(StatTabFactory($"{vm.Steps}", "Steps"), 0, 1);
        grid.Add(StatTabFactory($"{vm.Calories} kcal", "Calories"), 1, 1);
        ActivityData.Add(grid);
    }

    private VerticalStackLayout StatTabFactory(string mainText, string description, int textFontSize = 32, int descFontSize = 12)
    {
        var layout = new VerticalStackLayout
        {
            new Label() { Text = mainText,  HorizontalOptions = LayoutOptions.Center, FontSize = textFontSize},
            new Label() { Text = description, HorizontalOptions = LayoutOptions.Center, FontSize = descFontSize}
        };
        layout.Padding = new Thickness(20, 10, 20, 10);
        return layout;
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        vm.OnSliderValueChanged(e.NewValue);
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

