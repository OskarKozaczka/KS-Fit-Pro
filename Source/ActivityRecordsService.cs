using System.Xml.Serialization;

namespace KS_Fit_Pro.Source
{
    public class ActivityRecordsService
    {
        const string recordsFileName = "records.xml";

        public async Task<List<Activity>> LoadRecords()
        {
            var records = new List<Activity>();

            try
            {
                records = await ReadFromXmlFile<List<Activity>>(GetFilePath());
            }
            catch(Exception ex) { }
            return records;
        }

        public async void SaveRecords(List<Activity> records)
        {
#if ANDROID
            await Permissions.RequestAsync<StoragePermision>();
#endif
            WriteToXmlFile(GetFilePath(), records);
        }

        public string GetFilePath()
        {  
            var path = FileSystem.Current.AppDataDirectory;
            var fullPath = Path.Combine(path, recordsFileName);
            return fullPath;
        }

        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static async Task<T> ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }



    }

    public class Activity
    {
        public DateTime ActivityDate { get; set; }
        public int TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int TotalCalories { get; set; }
        public int TotalSteps { get; set; }
        public float AverageSpeed { get; set; }
        public List<int> SpeedHistory;
    }
}
