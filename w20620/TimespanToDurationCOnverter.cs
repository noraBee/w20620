using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace w20620
{
	[ValueConversion(typeof(TimeSpan), typeof(Duration))]  
	public class TimespanToDurationConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			var ts = (TimeSpan) value;
			return new Duration(ts);
		}

		public object ConvertBack(object value, Type targetType,
			object parameter, CultureInfo culture)
		{
			var ts = (Duration)value;
			return ts.TimeSpan;
		}
	}
}
