using KS_Fit_Pro.Source;
using Plugin.BLE.Abstractions.EventArgs;
using System.Collections.ObjectModel;

namespace KS_Fit_Pro.Pages;

public partial class ConnectionPage : ContentPage
{

    public ConnectionPage(ConnectionPageVM vm)
    {
        InitializeComponent();
        BindingContext = vm;

    }
}