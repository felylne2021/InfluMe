using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.Converters {
    /// <summary>
    /// This class have methods to convert the string values to image source. 
    /// </summary>
    [Preserve(AllMembers = true)]
    public class StringToImageSourceConverter : IValueConverter {
        /// <summary>
        /// This method is used to convert the string values to image source. 
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the color.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null) {
                var bytes = System.Convert.FromBase64String(value.ToString());
                return ImageSource.FromStream(() => new MemoryStream(bytes));
            }
            else
                return null;

        }

        /// <summary>
        /// This method is used to convert the color to bool.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the string.</returns>      
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
