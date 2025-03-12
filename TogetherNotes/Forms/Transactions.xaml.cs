using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace TogetherNotes.Forms
{
    public partial class Transactions : UserControl
    {
        private const double DefaultSize = 90;
        private const double SelectedSize = 110;
        private Image _selectedImage = null;

        public Transactions()
        {
            InitializeComponent();
            ResetFlagSizes();
        }

        private void SetCatalan(object sender, MouseButtonEventArgs e)
        {
            ((App)Application.Current).ChangeLanguage("ca");
            UpdateFlagSelection(ImgCatalan);
        }

        private void SetSpanish(object sender, MouseButtonEventArgs e)
        {
            ((App)Application.Current).ChangeLanguage("es");
            UpdateFlagSelection(ImgSpanish);
        }

        private void SetEnglish(object sender, MouseButtonEventArgs e)
        {
            ((App)Application.Current).ChangeLanguage("en");
            UpdateFlagSelection(ImgEnglish);
        }

        private void UpdateFlagSelection(Image selectedImage)
        {
            if (_selectedImage == selectedImage) return; // Evitem repetir l'animació

            _selectedImage = selectedImage;

            // Reset totes les banderes a mida per defecte
            ResetFlagSizes();

            // Ampliem només la seleccionada
            AnimateFlagSize(selectedImage, SelectedSize);
        }

        private void ResetFlagSizes()
        {
            AnimateFlagSize(ImgCatalan, DefaultSize);
            AnimateFlagSize(ImgSpanish, DefaultSize);
            AnimateFlagSize(ImgEnglish, DefaultSize);
        }

        private void AnimateFlagSize(Image img, double size)
        {
            var animation = new DoubleAnimation
            {
                To = size,
                Duration = TimeSpan.FromMilliseconds(200),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };

            img.BeginAnimation(WidthProperty, animation);
            img.BeginAnimation(HeightProperty, animation);
        }

        private void LogoutClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}