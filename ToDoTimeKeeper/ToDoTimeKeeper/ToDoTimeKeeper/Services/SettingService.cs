using System;
using System.Collections.Generic;
using System.Text;
using ToDoTimeKeeper.Infrastructure.Interfaces;
using ToDoTimeKeeper.Infrastructure.Models;
using ToDoTimeKeeper.Models;

namespace ToDoTimeKeeper.Services
{
    public class SettingService : ISettingService
    {
        public SettingService()
        {
            try
            {
                this.ReadFile();
            }
            catch(Exception e)
            {
                this.ToDoDataList = ToDoData.CreateDefault();
                this.SaveFile();
            }
        }

        public IEnumerable<ToDoData> ToDoDataList { get; private set; }

        public void SaveFile()
        {
            JsonSerializer.Serialize<IEnumerable<ToDoData>>(this.ToDoDataList, "ToDoDataList");
        }

        public void ReadFile()
        {
            var data = JsonSerializer.Deserialize<IEnumerable<ToDoData>>("ToDoDataList");
            this.ToDoDataList = data ?? ToDoData.CreateDefault();
        }
    }
}
