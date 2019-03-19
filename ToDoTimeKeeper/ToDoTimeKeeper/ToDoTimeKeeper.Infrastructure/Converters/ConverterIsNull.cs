using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ToDoTimeKeeper.Infrastructure.Converters
{
    public class ConverterIsNull : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isNull = value == null;
            return parameter == null? isNull : !isNull;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
