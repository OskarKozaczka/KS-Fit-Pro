using KS_Fit_Pro.Models;
using KS_Fit_Pro.ViewModels;
using Microsoft.Maui.Controls;
using Plugin.BLE.Abstractions.Contracts;
using System.Security.Principal;
using System.Windows.Input;

namespace KS_Fit_Pro.Pages;

public partial class ActivityRecordsPage : ContentPage
{
    ActivityRecordsPageVM vm;
    public ActivityRecordsPage(ActivityRecordsPageVM ViewModel)
	{
        InitializeComponent();
        vm = ViewModel;
        BindingContext = vm;
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        var button  = sender as Button;
        vm.DeleteRecord(button.BindingContext as Activity); 
    }

    async void GoToDetails(object sender, EventArgs args)
    {
        var record = sender as StackLayout;
        await Navigation.PushAsync(new RecordDetailsPage(new RecordDetailsPageVM(record.BindingContext as Activity)));
    }
}