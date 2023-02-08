using KS_Fit_Pro.Pages;
using KS_Fit_Pro.Source;
using KS_Fit_Pro.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace KS_Fit_Pro;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Configure();

        Routing.RegisterRoute("Filter", typeof(MainPage));

        return builder.Build();
	}
}
