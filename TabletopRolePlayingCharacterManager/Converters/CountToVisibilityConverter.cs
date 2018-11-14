using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TabletopRolePlayingCharacterManager.Converters
{
	class CountToVisibilityConverter : IValueConverter
	{
		//parameter is for reversing it.
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (value is int i)
			{
				if (bool.TryParse(parameter as string, out bool isReverse))
				{
					if (isReverse)
					{
						return i != 0 ? Visibility.Visible : Visibility.Collapsed;
					}

					return i == 0 ? Visibility.Visible : Visibility.Collapsed;
				}

				return i == 0 ? Visibility.Visible : Visibility.Collapsed;
			}

			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotSupportedException();
		}
	}
}
