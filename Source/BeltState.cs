namespace KS_Fit_Pro.Source
{
    public class BeltState
    {
        public BeltMode BeltMode { get; set; }
        public int BeltSpeed { get; set; }
        public bool ManualMode { get; set; }
        public TimeSpan ActivityTime { get; set; }
        public int ActivityDistance { get; set; }
        public int Steps { get; set; }
        public float AvgSpeed { get { return SpeedHistory.Sum() / SpeedHistory.Count; }}
        public List<int> SpeedHistory { get; set; }

        public BeltState() { }

        public BeltState(BeltMode beltMode, int beltSpeed, TimeSpan time, int distance, int steps, List<int> speedHistory)
        {
            BeltMode = beltMode;
            BeltSpeed = beltSpeed;
            ActivityTime= time;
            ActivityDistance = distance;
            Steps = steps;
            SpeedHistory= speedHistory;
        }

        public static BeltState operator +(BeltState a, BeltState b)
        {
            a.SpeedHistory.AddRange(b.SpeedHistory);

            return new BeltState(a.BeltMode, a.BeltSpeed,
                a.ActivityTime + b.ActivityTime,
                a.ActivityDistance + b.ActivityDistance,
                a.Steps + b.Steps, a.SpeedHistory);
        }
    }
}
