using System;
using System.Collections.Generic;
using System.Text;
using ToDoTimeKeeper.Models;
using Xamarin.Forms;

namespace ToDoTimeKeeper.TemplateSelectors
{
    class SettingItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SettingBoolTemplate { get; set; }
        public DataTemplate SettingTimeTemplate { get; set; }
        public DataTemplate SettingStringTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if(item is SettingItemTime)
            {
                return SettingTimeTemplate;
            }

            if(item is SettingItemBool)
            {
                return SettingBoolTemplate;
            }

            if (item is SettingItemString)
            {
                return SettingStringTemplate;
            }

            return null;
        }
    }
}
