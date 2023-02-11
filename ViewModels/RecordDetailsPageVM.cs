using KS_Fit_Pro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS_Fit_Pro.ViewModels
{
    public class RecordDetailsPageVM
    {

        public IDrawable Chart {get; set;}


        private Activity activity;
        public RecordDetailsPageVM(Activity activity)
        {
            this.activity = activity;
            Chart = ChartFactory(activity);
        }

        private IDrawable ChartFactory(Activity a)
        {
            return new ChartCreator(a);
        }
        private class ChartCreator : IDrawable
        {
            private Activity activity;
            public ChartCreator(Activity a)
            {
                this.activity = a;
                this.activity = new Activity() { SpeedHistory = new List<int> { 2, 2, 2, 2, 5, 5, 5, 5, 12, 12, 12 } };
            }
            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                if (this.activity == null) return;
                var stepSizeW = dirtyRect.Width / activity.SpeedHistory.Count;
                var stepSizeH = dirtyRect.Height / activity.SpeedHistory.Max();

                canvas.FillColor = Colors.White;
                canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);
                canvas.StrokeColor = Colors.Purple;
                canvas.StrokeSize = 6;

                var pos = 0;
                var last = 0;
                foreach (var entry in activity.SpeedHistory)
                {
                    if(last != entry)
                    {
                        canvas.DrawLine(pos * stepSizeW, dirtyRect.Height - last * stepSizeH, stepSizeW * pos, dirtyRect.Height - entry * stepSizeH);
                    }
                    canvas.DrawLine(pos * stepSizeW, dirtyRect.Height - entry * stepSizeH, stepSizeW*pos + stepSizeW, dirtyRect.Height - entry * stepSizeH);
                    pos += 1;
                    last = entry;
                }
            }
        }
    }
}
