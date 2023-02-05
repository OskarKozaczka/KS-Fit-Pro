using KS_Fit_Pro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS_Fit_Pro.Source
{
    public class CaloriesCalculator
    {
        private readonly OptionsPageVM _options;
        public CaloriesCalculator(OptionsPageVM options)
        {
            _options = options;
        }
        internal int CalculateCalories(BeltState currentBeltState)
        {
            return (int)(currentBeltState.AvgSpeed * _options.Weight * 3.5 / 200 * currentBeltState.ActivityTime.TotalSeconds);
        }
    }
}
