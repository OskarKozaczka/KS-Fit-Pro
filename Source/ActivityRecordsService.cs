using System.Xml.Serialization;
using KS_Fit_Pro.Models;

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
                if (File.Exists(GetFilePath()))
                {
                    records = await ReadFromXmlFile<List<Activity>>(GetFilePath());
                }
                
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
}
