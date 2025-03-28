using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using TogetherNotes.Models.Management;

namespace TogetherNotes.ViewModel
{
    /// <summary>
    /// ViewModel para la vista del mapa.
    /// Gestiona la colección de marcadores en el mapa obtenidos desde la base de datos.
    /// </summary>
    class MapVM : INotifyPropertyChanged
    {
        // Colección de marcadores que serán mostrados en el mapa
        private ObservableCollection<GMapMarker> _markers;

        /// <summary>
        /// Propiedad que representa la colección de marcadores en el mapa.
        /// </summary>
        public ObservableCollection<GMapMarker> Markers
        {
            get { return _markers; }
            set
            {
                _markers = value;
                OnPropertyChanged(nameof(Markers)); // Notifica cualquier cambio en la colección de marcadores
            }
        }

        // Evento para notificar cambios de propiedades (para implementar INotifyPropertyChanged)
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Método que invoca el evento PropertyChanged cuando una propiedad cambia.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que ha cambiado.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Constructor del ViewModel que inicializa la colección de marcadores y carga las ubicaciones desde la base de datos.
        /// </summary>
        public MapVM()
        {
            // Inicializa la colección de marcadores
            Markers = new ObservableCollection<GMapMarker>();

            // Carga las ubicaciones desde la base de datos y las convierte en marcadores
            LoadLocationsFromDB();
        }

        /// <summary>
        /// Método que obtiene las ubicaciones desde la base de datos y las convierte en marcadores en el mapa.
        /// </summary>
        private void LoadLocationsFromDB()
        {
            // Obtiene todas las ubicaciones de la base de datos
            var locations = AppOrm.SelectAllLocations();

            // Recorre cada ubicación y crea un marcador correspondiente
            foreach (var location in locations)
            {
                // Crea una imagen que representará el marcador en el mapa
                var image = new Image
                {
                    // Establece la imagen del marcador (usando una imagen desde los recursos del proyecto)
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/marker.png")),
                    Width = 30, // Ancho del marcador
                    Height = 30 // Alto del marcador
                };

                // Crea un marcador con la ubicación obtenida de la base de datos
                var marker = new GMapMarker(new PointLatLng(location.Latitude, location.Longitude))
                {
                    Shape = image // Asocia la imagen al marcador
                };

                // Añade el marcador a la colección de marcadores
                Markers.Add(marker);
            }
        }
    }
}
