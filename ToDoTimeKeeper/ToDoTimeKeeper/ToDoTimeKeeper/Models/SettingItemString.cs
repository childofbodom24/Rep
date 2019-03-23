using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTimeKeeper.Models
{
    public class SettingItemString : SettingItem<string>
    {
        public SettingItemString(string name) : base(name, false) { }
    }
}
