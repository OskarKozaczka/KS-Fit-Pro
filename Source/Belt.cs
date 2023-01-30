namespace KS_Fit_Pro.Source
{
    internal class BeltState
    {
        public BeltMode beltMode { get; set; }
        public int beltSpeed { get; set; }
        public bool manualMode { get; set; }
        public TimeSpan activityTime { get; set; }
        public int activityDistance { get; set; }
        public int steps { get; set; }
    }
}
