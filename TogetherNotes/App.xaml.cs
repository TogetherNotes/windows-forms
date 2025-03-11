using System;
using System.Linq;
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

            // Manté la resta de recursos i només substitueix el d'idioma
            var existingDictionaries = Application.Current.Resources.MergedDictionaries
                                         .Where(d => !d.Source.OriginalString.Contains("Languages/strings"))
                                         .ToList();

            Application.Current.Resources.MergedDictionaries.Clear();

            // Reafegir els diccionaris existents
            foreach (var dict in existingDictionaries)
            {
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }

            // Afegir el nou diccionari d'idioma
            Application.Current.Resources.MergedDictionaries.Add(newDictionary);
        }
    }
}