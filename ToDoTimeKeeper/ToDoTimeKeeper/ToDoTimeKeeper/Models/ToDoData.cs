using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace ToDoTimeKeeper.Models
{
    public class ToDoData
    {
        private static Dictionary<DayOfWeek, string> dayOfWeekDictionary = new Dictionary<DayOfWeek, string>()
        {
            { DayOfWeek.Monday, "月" },
            { DayOfWeek.Tuesday, "火" },
            { DayOfWeek.Wednesday, "水" },
            { DayOfWeek.Thursday, "木" },
            { DayOfWeek.Friday, "金" },
            { DayOfWeek.Saturday, "土" },
            { DayOfWeek.Sunday, "日" }
        };

        public ToDoData(string name)
        {
            this.NameTag.Data = name;

            this.WeekDays = new SettingItemBool[Enum.GetValues(typeof(DayOfWeek)).GetLength(0)];
            foreach (var d in Enum.GetValues(typeof(DayOfWeek)))
            {
                this.WeekDays[(int)d] = new SettingItemBool(dayOfWeekDictionary[(DayOfWeek)d]);
                this.WeekDays[(int)d].IsChecked = true;
            }
        }
        
        public SettingItemString NameTag { get; set; } = new SettingItemString("名前");
        public SettingItemTime StartTime { get; set; } = new SettingItemTime("開始時間", false);
        public SettingItemTime EndTime { get; set; } = new SettingItemTime("終了時間", false);

        public SettingItemBool[] WeekDays { get; set; }

        public SettingItemString[] CheckItems { get; set; } = Enumerable.Range(1, 8).Select(i => new SettingItemString($"項目{i}")).ToArray();

        [JsonIgnore]
        public IEnumerable<SettingItemBase> SettingItems
        {
            get
            {
                var items = new List<SettingItemBase>();
                items.Add(NameTag);
                items.Add(StartTime);
                items.Add(EndTime);
                items.AddRange(WeekDays);
                items.AddRange(CheckItems);

                return items;
            }
        }

        public static IEnumerable<ToDoData> CreateDefault()
        {
            var result = new List<ToDoData>();

            var morning = new ToDoData("朝準備");
            morning.StartTime.Data = new TimeSpan(7, 20, 0);
            morning.EndTime.Data = new TimeSpan(7, 50, 0);
            morning.WeekDays[(int)DayOfWeek.Saturday].IsChecked = false;
            morning.WeekDays[(int)DayOfWeek.Sunday].IsChecked = false;
            morning.CheckItems[0].Data = "A";
            morning.CheckItems[1].Data = "B";
            morning.CheckItems[2].Data = "C";
            result.Add(morning);

            var kitaku = new ToDoData("帰宅後");
            kitaku.StartTime.Data = new TimeSpan(15, 00, 0);
            kitaku.EndTime.Data = new TimeSpan(16, 00, 0);
            kitaku.WeekDays[(int)DayOfWeek.Saturday].IsChecked = false;
            kitaku.WeekDays[(int)DayOfWeek.Sunday].IsChecked = false;
            kitaku.CheckItems[0].Data = "D";
            kitaku.CheckItems[1].Data = "E";
            kitaku.CheckItems[2].Data = "F";
            result.Add(kitaku);

            var night = new ToDoData("寝る前");
            night.StartTime.Data = new TimeSpan(20, 00, 0);
            night.EndTime.Data = new TimeSpan(21, 00, 0);
            night.CheckItems[0].Data = "G";
            night.CheckItems[1].Data = "H";
            night.CheckItems[2].Data = "I";
            result.Add(night);

            return result;
        }
    }
}
