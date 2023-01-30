using KS_Fit_Pro.Source;

namespace KS_Fit_Pro.Pages;

public partial class AppTabbedPage 
{
    public AppTabbedPage(MainPage activityPage, ConnectionPage connectionPage)
    {
        InitializeComponent();

        var FirstPage = new NavigationPage(activityPage);
        FirstPage.Title = "Activity";
        this.Children.Add(FirstPage);

        var SecondPage = new NavigationPage(connectionPage);
        SecondPage.Title = "Connection";
        this.Children.Add(SecondPage);
    }
}
