using KS_Fit_Pro.Pages;
using KS_Fit_Pro.Source;
using KS_Fit_Pro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS_Fit_Pro
{
    public static class ConfigureModules
    {
        public static MauiAppBuilder Configure(this MauiAppBuilder builder)
        {
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

            builder.Services.AddSingleton<RecordDetailsPage>();
            builder.Services.AddSingleton<RecordDetailsPageVM>();

            builder.Services.AddSingleton<AppTabbedPage>();

            return builder;
        }
    }

}
