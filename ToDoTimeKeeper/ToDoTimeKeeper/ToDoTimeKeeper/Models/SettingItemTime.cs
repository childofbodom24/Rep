using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTimeKeeper.Models
{
    public class SettingItemTime : SettingItem<TimeSpan>
    {
        public SettingItemTime(string name, bool checkable) : base(name, checkable) { }
    }
}
