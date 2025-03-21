using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using TogetherNotes.Models.Management;

namespace TogetherNotes.ViewModel
{
    class MapVM : INotifyPropertyChanged
    {
        private ObservableCollection<GMapMarker> _markers;
        public ObservableCollection<GMapMarker> Markers
        {
            get { return _markers; }
            set
            {
                _markers = value;
                OnPropertyChanged(nameof(Markers));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MapVM()
        {
            Markers = new ObservableCollection<GMapMarker>();
            LoadLocationsFromDB();
        }

        private void LoadLocationsFromDB()
        {
            // Obtenir dades de la BD
            var locations = AppOrm.SelectAllLocations();

            foreach (var location in locations)
            {
                // Crear la icona del marcador
                var image = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/marker.png")),
                    Width = 30,
                    Height = 30
                };

                // Crear i afegir el marcador
                var marker = new GMapMarker(new PointLatLng(location.Latitude, location.Longitude))
                {
                    Shape = image
                };

                Markers.Add(marker);
            }
        }
    }
}