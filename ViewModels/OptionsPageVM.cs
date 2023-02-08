using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS_Fit_Pro.ViewModels
{
    public class OptionsPageVM
    {
        public int Weight { get; set; }

        public OptionsPageVM()
        {
            Weight = Convert.ToInt32(SecureStorage.Default.GetAsync("weight").Result);
        }

        internal async void Save(object sender, EventArgs e)
        {
            await SecureStorage.Default.SetAsync("weight", Weight.ToString());
            var entry = sender as Entry; 
#if ANDROID
	        Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
            entry.Unfocus();
        }

        internal void SwitchMode(bool value)
        {
            Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
        }
    }
}
