namespace KS_Fit_Pro.Pages;

public partial class App : Application
{
    public App(AppTabbedPage page)
    {
        InitializeComponent();

        MainPage = page;
    }
}
