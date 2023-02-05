﻿using KS_Fit_Pro.Pages;
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

		builder.Services.AddSingleton<BLEConnector>();
        builder.Services.AddSingleton<BeltController>();
        builder.Services.AddSingleton<BeltRequestReceiver>();
        builder.Services.AddSingleton<ActivityRecordsService>();
        builder.Services.AddSingleton<CaloriesCalculator>();

        builder.Services.AddSingleton<ActivityRecordsPage>();
        builder.Services.AddSingleton<ActivityRecordsPageVM>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageVM>();

        builder.Services.AddSingleton<ConnectionPage>();
        builder.Services.AddSingleton<ConnectionPageVM>();

        builder.Services.AddSingleton<OptionsPage>();
        builder.Services.AddSingleton<OptionsPageVM>();

        builder.Services.AddSingleton<AppTabbedPage>();

        Routing.RegisterRoute("Filter", typeof(MainPage));

        return builder.Build();
	}
}
