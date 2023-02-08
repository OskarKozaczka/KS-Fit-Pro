using KS_Fit_Pro.Source;
using KS_Fit_Pro.ViewModels;

namespace KS_Fit_Pro.Pages;

public partial class RecordDetailsPage : ContentPage
{
    RecordDetailsPageVM vm;
    public RecordDetailsPage(RecordDetailsPageVM vm)
	{
		InitializeComponent();
		this.vm = vm;
        BindingContext = vm;
    }
}