using KS_Fit_Pro.Source;
using KS_Fit_Pro.ViewModels;

namespace KS_Fit_Pro.Pages;

public partial class AppTabbedPage 
{
    ConnectionPageVM vm;

    public AppTabbedPage(MainPage activityPage, ConnectionPage connectionPage, ConnectionPageVM vm, ActivityRecordsPage activityRecordsPage, OptionsPage optionsPage)
    {
        InitializeComponent();

        Children.Add(PageFactory(activityPage, "Activity", "/Images/run.svg"));
        Children.Add(PageFactory(activityRecordsPage, "Records", "/Images/list.svg"));
        Children.Add(PageFactory(connectionPage, "Connection", "/Images/bt.svg"));
        Children.Add(PageFactory(optionsPage, "Options", "/Images/settings.svg"));

        CurrentPageChanged += CurrentPageHasChanged;

        this.vm = vm;
    }

    private async void CurrentPageHasChanged(object sender, EventArgs e)
    {
        var tabbedPage = (TabbedPage)sender;
        Title = tabbedPage.CurrentPage.Title;

        if (Title == "Connection") await vm.StartScan();
        else vm.StopScan();
    }

    private NavigationPage PageFactory(Page root, string title, string iconPath)
    {
        var page = new NavigationPage(root);
        page.Title = title;
        page.IconImageSource = iconPath;
        return page;
    }
}
