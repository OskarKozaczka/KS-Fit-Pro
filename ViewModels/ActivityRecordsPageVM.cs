using KS_Fit_Pro.Source;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace KS_Fit_Pro.ViewModels
{
    public class ActivityRecordsPageVM
    {
        public ObservableCollection<Activity> ActivityRecords { get; set; }
        public int TotalDistanceSum { get; set; }
        public TimeSpan TotalTimeSum { get; set; }
        public int TotalCaloriesSum { get; set; }

        private readonly ActivityRecordsService _service;

        public ActivityRecordsPageVM(ActivityRecordsService service)
        {
            _service = service;
            LoadActivities();
            ActivityRecords.Select(x => TotalCaloriesSum += x.TotalCalories);
            ActivityRecords.Select(x => TotalDistanceSum += x.TotalDistance);
            ActivityRecords.Select(x => TotalTimeSum += x.TotalTime);

            ActivityRecords.CollectionChanged += Save;
        }

        private void Save(object sender, NotifyCollectionChangedEventArgs e)
        {
            _service.SaveRecords(ActivityRecords.ToList());
        }

        public async void LoadActivities()
        {
            ActivityRecords = new ObservableCollection<Activity>(await _service.LoadRecords());
        }

        internal void DeleteRecord(Activity activity)
        {
            ActivityRecords.Remove(activity); 
        }
    }
}
