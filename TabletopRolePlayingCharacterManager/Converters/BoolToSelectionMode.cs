using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace TabletopRolePlayingCharacterManager.Converters
{
	/// <summary>
	/// Converts a bool to selectionmode, and vice versa. true for selecting multiple, false for single selection.
	/// </summary>
	public class BoolToSelectionMode : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var mode = SelectionMode.Single;
			if (value is bool)
			{
				mode = (bool)value ? SelectionMode.Multiple : SelectionMode.Single;
			}
			return mode;

		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			var result = false;
			if (value is SelectionMode)
			{
				result = (SelectionMode)value == SelectionMode.Multiple;
			}
			return result;
		}
	}
}
