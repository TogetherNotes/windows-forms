using System;
using System.Windows;

namespace TogetherNotes
{
    public partial class App : Application
    {
        public void ChangeLanguage(string languageCode)
        {
            string dictionaryPath = $"Languages/strings.{languageCode}.xaml";

            ResourceDictionary newDictionary = new ResourceDictionary
            {
                Source = new Uri(dictionaryPath, UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newDictionary);
        }
    }
}