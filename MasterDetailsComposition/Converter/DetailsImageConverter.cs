using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MasterDetailsComposition.Converter
{
    public class DetailsImageConverter : IValueConverter
    {
        string uriprefix = "ms-appx:///";
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var path = value as String;
            var source = new Uri($"{uriprefix}Assets/Details/{path}.jpg");
            return new BitmapImage(source);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
