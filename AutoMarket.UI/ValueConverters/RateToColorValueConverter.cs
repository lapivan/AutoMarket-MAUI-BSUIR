using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AutoMarket.UI.ValueConverters
{
    public class PriceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {

            if ((decimal)value < 70000)
                return Colors.LightPink;
            return Colors.WhiteSmoke;     
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
