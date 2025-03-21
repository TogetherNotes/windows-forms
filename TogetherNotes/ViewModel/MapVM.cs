using System.Collections.ObjectModel;
using System.ComponentModel;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows.Media;
using System.Windows.Shapes;
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
                var marker = new GMapMarker(new PointLatLng(location.Latitude, location.Longitude))
                {
                    Shape = new Ellipse
                    {
                        Width = 12,
                        Height = 12,
                        Stroke = Brushes.White,
                        StrokeThickness = 2,
                        Fill = Brushes.Red,
                    }
                };

                Markers.Add(marker);
            }
        }
    }
}