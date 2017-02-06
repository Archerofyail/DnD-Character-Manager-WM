using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TabletopRolePlayingCharacterManager.Controls
{
	public sealed partial class TabHeader : UserControl
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
			this.InitializeComponent();
			this.DataContext = this;
		}
	}
}
