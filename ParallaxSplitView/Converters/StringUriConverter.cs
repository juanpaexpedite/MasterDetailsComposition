using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ParallaxSplitView.Converters
{
    public class StringUriConverter : IValueConverter
    {
        private Uri CreateUri(string input)
        {
            if (input.Contains("ms-appx"))
            {
                return new Uri(input);
            }
            else
            {
                return new Uri($"ms-appx:///{input.TrimStart('/')}");
            }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return CreateUri((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
