using System;
using Cirrious.CrossCore.Converters;
using System.Globalization;

namespace Test.iOS
{
	public class InvertBooleanValueConverter : IMvxValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !((bool)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !((bool)value);
		}
	}
}

