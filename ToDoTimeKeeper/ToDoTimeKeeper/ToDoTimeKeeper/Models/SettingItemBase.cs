using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoTimeKeeper.Infrastructure.Models;

namespace ToDoTimeKeeper.Models
{
    public abstract class SettingItemBase : BindableBaseEx
    {
        private bool isChecked;

        public SettingItemBase(string name, bool checkable)
        {
            this.Name = name;
            this.Checkable = checkable;
            this.IsChecked = true;
        }
        public string Name { get; set; }

        [JsonIgnore]
        public bool Checkable { get; }

        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }
        }

        [JsonIgnore]
        public bool IsEnabled { get => Checkable ? IsChecked : true; }
    }
}
