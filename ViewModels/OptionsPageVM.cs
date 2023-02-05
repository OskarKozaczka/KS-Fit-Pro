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
        }
    }
}
