using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace TabletopRolePlayingCharacterManager.Converters
{
	public class BoolToSelectionMode : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			SelectionMode mode = SelectionMode.Single;
			if (value is bool)
			{
				mode = (bool)value ? SelectionMode.Multiple : SelectionMode.Single;
			}
			return mode;

		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			bool result = false;
			if (value is SelectionMode)
			{
				result = (SelectionMode)value == SelectionMode.Multiple;
			}
			return result;
		}
	}
}
