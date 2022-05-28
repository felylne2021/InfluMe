﻿using InfluMe.Helpers;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace InfluMe.Converters
{
    /// <summary>
    /// This class have methods convert to reverse the Boolean values.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class StatusOngoingBoolConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert to reverse the Boolean values.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the  inverse bool value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
