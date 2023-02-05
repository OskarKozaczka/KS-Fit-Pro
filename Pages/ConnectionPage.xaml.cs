using KS_Fit_Pro.Source;
using KS_Fit_Pro.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace KS_Fit_Pro.Pages;

public partial class ConnectionPage : ContentPage
{
    ConnectionPageVM vm;
    public ConnectionPage(ConnectionPageVM ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
        vm = ViewModel;
    }
    


    private async void Button_Clicked(object sender, EventArgs e)
    {
        var succes = await vm.ButtonClicked((IDevice)((Button)sender).BindingContext);
        if (succes) ((Button)sender).BackgroundColor = Color.Parse("green");

    }
}