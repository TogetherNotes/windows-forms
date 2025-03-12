using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace TogetherNotes.Forms
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SetLanguage(string languageCode)
        {
            CultureInfo culture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            ResourceDictionary dict = new ResourceDictionary
            {
                Source = new Uri($"/Languages/strings.{languageCode}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void SetCatalan(object sender, RoutedEventArgs e)
        {
            SetLanguage("ca");
        }

        private void SetSpanish(object sender, RoutedEventArgs e)
        {
            SetLanguage("es");
        }

        private void SetEnglish(object sender, RoutedEventArgs e)
        {
            SetLanguage("en");
        }
    }
}