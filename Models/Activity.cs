namespace KS_Fit_Pro.Models
{
    public class Activity
    {
        public DateTime ActivityDate { get; set; }
        public int TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int TotalCalories { get; set; }
        public int TotalSteps { get; set; }
        public float AverageSpeed { get; set; }
        public List<int> SpeedHistory { get; set; }
    }
}
