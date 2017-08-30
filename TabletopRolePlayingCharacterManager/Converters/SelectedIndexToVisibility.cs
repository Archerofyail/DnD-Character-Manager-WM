using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TabletopRolePlayingCharacterManager.Converters
{
	class SelectedIndexToVisibility : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var vis = Visibility.Collapsed;
			if (value is int)
			{
				vis = (int) value == -1 ? Visibility.Collapsed : Visibility.Visible;
			}

			return vis;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
