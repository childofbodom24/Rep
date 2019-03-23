using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTimeKeeper.Models
{
    public class SettingItemBool : SettingItemBase
    {
        public SettingItemBool(string name) : base(name, true) { }
    }
}
