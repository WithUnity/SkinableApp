/* Copyright 2018-2021 Robin Murison

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SkinnableApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CTOR Exception: " + ex.Message);
            }
            // Load the default skin.
            Grid mainGrid = Content as Grid;
            MenuItem languageMenuItem = mainGrid.ContextMenu.Items[5] as MenuItem;
            ApplyLanguageFromMenuItem(languageMenuItem);
            MenuItem skinMenuItem = mainGrid.ContextMenu.Items[0] as MenuItem;
            ApplySkinFromMenuItem(skinMenuItem);
        }

        public string SkinResources => @"./Resources/Skins/BlueSkin.xaml";
        public string LanguageResources => @"./Resources/Languages/English.xaml";

        private void OnContextMenuSkinsClick(Grid mainGrid, MenuItem menuItem)
        {
            // Update the checked state of the menu items.
            foreach (MenuItem mi in mainGrid.ContextMenu.Items.OfType<MenuItem>().Where(skinItem => (skinItem.Tag as string).Contains("Skins")))
            {
                mi.IsChecked = (mi.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
            }
            //            foreach (var child in mainGrid.Children)
            try
            {
                var mainMenu = mainGrid.Children.OfType<Menu>().Single(child => child is Menu);
                var skinMenu = mainMenu.Items.OfType<MenuItem>().Single(item => item.Name == "SkinsMenu");
                foreach (MenuItem mi in skinMenu.Items.OfType<MenuItem>())
                {
                    mi.IsChecked = (mi.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
                }
            }
            catch { }
            // Load the selected skin.
            ApplySkinFromMenuItem(menuItem);
        }

        void OnContextMenuLanguagesClick(Grid mainGrid, MenuItem menuItem)
        {
            // Update the checked state of the context menu items.
            foreach (MenuItem mi in mainGrid.ContextMenu.Items.OfType<MenuItem>().Where(skinItem => (skinItem.Tag as string).Contains("Languages")))
            {
                mi.IsChecked = (mi.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
            }
            //            foreach (var child in mainGrid.Children)
            try
            {
                var mainMenu = mainGrid.Children.OfType<Menu>().Single(child => child is Menu);
                var languageMenu = mainMenu.Items.OfType<MenuItem>().Single(item => item.Name == "LanguagesMenu");
                foreach (MenuItem mi in languageMenu.Items.OfType<MenuItem>())
                {
                    mi.IsChecked = (mi.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
                }
            }
            catch { }
            // Load the selected skin.
            ApplyLanguageFromMenuItem(menuItem);
        }

        void OnContextMenuItemClick(object sender, RoutedEventArgs e)
		{
			MenuItem contextMenuItem = e.OriginalSource as MenuItem;
            Grid mainGrid = Content as Grid;
            if ((contextMenuItem.Tag as string).Contains("Skins"))
            {
                OnContextMenuSkinsClick(mainGrid, contextMenuItem);
            }
            else if ((contextMenuItem.Tag as string).Contains("Languages"))
            {
                OnContextMenuLanguagesClick(mainGrid, contextMenuItem);
            }
		}

        void OnSkinClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            ApplySkinFromMenuItem(menuItem);
            Grid mainGrid = Content as Grid;
            // Update the checked state of the context menu items.
            foreach (MenuItem mi in mainGrid.ContextMenu.Items.OfType<MenuItem>().Where(item => (item.Tag as string).Contains("Skins")))
            {
                mi.IsChecked = (mi.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
            }
            //            foreach (var child in mainGrid.Children)
            try
            {
                var mainMenu = mainGrid.Children.OfType<Menu>().Single(child => child is Menu);
                var skinsMenu = mainMenu.Items.OfType<MenuItem>().Single(item => item.Name == "SkinsMenu");
                foreach (MenuItem mi in skinsMenu.Items.OfType<MenuItem>())
                {
                    mi.IsChecked = (mi.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
                }
            }
            catch { }
        }

        void OnLanguageClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            ApplyLanguageFromMenuItem(menuItem);
            Grid mainGrid = Content as Grid;
            // Update the checked state of the context menu items.
            foreach (MenuItem mi in mainGrid.ContextMenu.Items.OfType<MenuItem>().Where(item => (item.Tag as string).Contains("Languages")))
            {
                mi.IsChecked = (mi.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
            }
            //            foreach (var child in mainGrid.Children)
            try
            {
                var mainMenu = mainGrid.Children.OfType<Menu>().Single(child => child is Menu);
                var languageMenu = mainMenu.Items.OfType<MenuItem>().Single(item => item.Name == "LanguagesMenu");
                MenuItem mit;
                foreach (MenuItem mi in languageMenu.Items.OfType<MenuItem>())
                {
                    mit = mi;
                    mit.IsChecked = (mit.Tag as string).ToLower().Equals((menuItem.Tag as string).ToLower());
                }
            }
            catch(Exception exception)
            {
                System.Console.WriteLine($"{exception.GetType().Name}: {exception.Message}");
            }
        }

        void ApplySkinFromMenuItem(MenuItem item)
		{
			// Get a relative path to the ResourceDictionary which
			// contains the selected skin.
			string skinDictPath = item.Tag as string;
            string coloursDictPath = skinDictPath.Replace("Skin.", "Colours.");
            Uri ColourDictUri = new Uri(coloursDictPath, UriKind.Relative);

            Uri skinDictUri = new Uri(skinDictPath, UriKind.Relative);

			// Tell the Application to load the skin resources.
			DemoApp app = Application.Current as DemoApp;
			app.ApplySkin(ColourDictUri, skinDictUri);
		}

        void ApplyLanguageFromMenuItem(MenuItem item)
        {
            // Get a relative path to the ResourceDictionary which
            // contains the selected skin.
            string languageDictPath = item.Tag as string;
            Uri languageDictUri = new Uri(languageDictPath, UriKind.Relative);

            // Tell the Application to load the skin resources.
            DemoApp app = Application.Current as DemoApp;
            app.ApplyLanguage(languageDictUri);
        }
    }
}