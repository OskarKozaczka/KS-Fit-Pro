﻿using KS_Fit_Pro.Pages;
using KS_Fit_Pro.Source;
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

		builder.Services.AddSingleton<BLEConnector>();
        builder.Services.AddSingleton<BeltController>();
        builder.Services.AddSingleton<BeltRequestReceiver>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageVM>();
        builder.Services.AddSingleton<ConnectionPage>();
        builder.Services.AddSingleton<ConnectionPageVM>();
        builder.Services.AddSingleton<AppTabbedPage>();

        Routing.RegisterRoute("Filter", typeof(MainPage));

        return builder.Build();
	}
}
