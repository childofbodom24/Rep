using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoTimeKeeper.Models
{
    public class SettingItem<T> : SettingItemBase
    {
        private T data;

        public SettingItem(string name, bool checkable) : base(name, checkable) { }

        public T Data
        {
            get
            {
                return data;
            }

            set
            {
                if (this.SetProperty(ref data, value))
                {
                    this.IsChecked = true;
                }
            }
        }
    }
}
