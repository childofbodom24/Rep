using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace ToDoTimeKeeper.Infrastructure.Converters
{
    public class ConverterDayOfWeek : IValueConverter
    {
        private Dictionary<DayOfWeek, string> dict = new Dictionary<DayOfWeek, string>()
        {
            { DayOfWeek.Monday, "月" },
            { DayOfWeek.Tuesday, "火" },
            { DayOfWeek.Wednesday, "水" },
            { DayOfWeek.Thursday, "木" },
            { DayOfWeek.Friday, "金" },
            { DayOfWeek.Saturday, "土" },
            { DayOfWeek.Sunday, "日" }
        };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return dict[(DayOfWeek)value];
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (DayOfWeek)dict.First(kv=> kv.Value == (string)value).Key;
        }
    }
}
