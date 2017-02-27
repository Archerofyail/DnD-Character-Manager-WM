using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TabletopRolePlayingCharacterManager.Controls
{
	public sealed partial class TabHeader
	{
		public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Symbol", typeof(Symbol), typeof(TabHeader), null);

		public Symbol Symbol
		{
			get
			{
				//Symbol result = Symbol.Accept;
				//if (Enum.TryParse(GetValue(GlyphProperty), out result))
				//{
				//	Debug.WriteLine("Result is " + result);
				//	return result;
				//}
				return (Symbol)GetValue(GlyphProperty);
			}
			set { SetValue(GlyphProperty, value); }
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TabHeader), null);

		public string Label
		{
			get { return GetValue(LabelProperty) as string; }
			set { SetValue(LabelProperty, value); }
		}


		public TabHeader()
		{
			InitializeComponent();
			DataContext = this;
		}
	}
}
