using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;

namespace Make10.Models
{
    public static class JsonSerializer
    {
        public static void Serialize<T>(T data, string filename)
        {
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(GetApplicationFilePath(filename), json);
        }
        public static T Deserialize<T>(string filename) where T : class
        {
            var filepath = GetApplicationFilePath(filename);
            if (File.Exists(filepath))
            {
                var json = File.ReadAllText(GetApplicationFilePath(filename));
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return null;
            }
        }

        public static string GetApplicationFilePath(string filename)
        {
            var folderpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}
