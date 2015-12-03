﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DnD_Character_Manager.Types;
using DnD_Character_Manager.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DnD_Character_Manager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
			var resourcenames = GetType().GetTypeInfo().Assembly.GetManifestResourceNames();
			 
	        if (resourcenames.Length == 0)
	        {
		        Debug.WriteLine("resourceNames is empty");
	        }
			foreach (var name in resourcenames)
			{
				Debug.WriteLine("Found resource name: " + name);

			}
			//JsonLoader.resourceLoader = ResourceLoader.GetForViewIndependentUse();
		}

	    private void AddButtonClicked(object sender, RoutedEventArgs e)
	    {
			
		    Frame.Navigate(typeof (AddNewCharacter));
	    }
    }
}
