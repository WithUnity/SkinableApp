/* Copyright 2018-2021 Robin Murison

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SkinnableApp
{
    public partial class DemoApp : Application
	{
        public DemoApp()
        {
            var skinDictPath = @".\Resources\Skins\BlackSkin.xaml";
            string coloursDictPath = skinDictPath.Replace("Skin.xaml", "Colours.xaml");
            Uri ColourDictUri = new Uri(coloursDictPath, UriKind.Relative);

            Uri skinDictUri = new Uri(skinDictPath, UriKind.Relative);

            // Tell the Application to load the skin resources.
            ApplySkin(ColourDictUri, skinDictUri);
            var languageDictPath = @".\Resources\Languages\English.xaml";
            Uri languageDictUri = new Uri(languageDictPath, UriKind.Relative);

            // Tell the Application to load the skin resources.
            ApplyLanguage(languageDictUri);
        }
        public void ApplySkin(Uri coloursDictionaryUri, Uri skinDictionaryUri)
		{
            // Load the ResourceDictionary into memory.
            ResourceDictionary coloursDictionary = LoadComponent(coloursDictionaryUri) as ResourceDictionary;
            ResourceDictionary skinDictionary = LoadComponent(skinDictionaryUri) as ResourceDictionary;

			Collection<ResourceDictionary> mergedDicts = Resources.MergedDictionaries;

            // Remove the existing skin dictionary, if one exists.
            ResourceDictionary oldColoursDictionary = mergedDicts.FirstOrDefault(dict => dict.Contains("InfoTextBrush"));

            // Apply the selected colour so that all elements in the
            // application will honor the new colours.
            mergedDicts.Add(coloursDictionary);
            if (oldColoursDictionary != null)
            {
                mergedDicts.Remove(oldColoursDictionary);
            }

            // Remove the existing skin dictionary, if one exists.
            ResourceDictionary oldSkinDictionary = mergedDicts.FirstOrDefault(dict => dict.Contains("StyleBackground")); 

            // Apply the selected skin so that all elements in the
            // application will honor the new look and feel.
            mergedDicts.Add(skinDictionary);
            if (oldSkinDictionary != null)
            {
                mergedDicts.Remove(oldSkinDictionary);
            }
        }

        public void ApplyLanguage(Uri languageDictionaryUri)
        {
            // Load the ResourceDictionary into memory.
            ResourceDictionary languageDict = Application.LoadComponent(languageDictionaryUri) as ResourceDictionary;

            Collection<ResourceDictionary> mergedDicts = base.Resources.MergedDictionaries;

            ResourceDictionary languageDictionary = null;

            // Remove the existing skin dictionary, if one exists.
            // NOTE: In a real application, this logic might need
            // to be more complex, because there might be dictionaries
            // which should not be removed.
            foreach (var dict in mergedDicts)
            {
                if (dict.Contains("Agent"))
                {
                    languageDictionary = dict;
                }
            }

            // Apply the selected skin so that all elements in the
            // application will honor the new look and feel.
            mergedDicts.Add(languageDict);
            if (languageDictionary != null)
            {
                mergedDicts.Remove(languageDictionary);
            }
        }
    }
}