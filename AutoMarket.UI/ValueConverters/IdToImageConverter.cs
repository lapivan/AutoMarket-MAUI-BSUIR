using System.Globalization;

namespace AutoMarket.UI.ValueConverters
{

    public class IdToImageConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int id)
            {
                string path = ImagePathHelper.GetImagePath(id);

                if (File.Exists(path))
                {
                    return ImageSource.FromFile(path);
                }
            }
            return ImageSource.FromFile("placeholder.png");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
