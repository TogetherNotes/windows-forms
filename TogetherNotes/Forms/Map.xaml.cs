using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using GMap.NET;
using GMap.NET.MapProviders;
using TogetherNotes.ViewModel;

namespace TogetherNotes.Forms
{
    public partial class Map : UserControl
    {
        private readonly RectangleGeometry clipGeometry;
        private MapVM _viewModel;

        public Map()
        {
            InitializeComponent();

            // Inicialitzem el Clip
            clipGeometry = new RectangleGeometry();
            MapControl.Clip = clipGeometry;

            // Inicialitzem el ViewModel
            _viewModel = new MapVM();
            this.DataContext = _viewModel;

            // Events
            MapControl.Loaded += MapControl_Loaded;
            MapControl.SizeChanged += MapControl_SizeChanged;
        }

        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Carreguem i configurem el mapa només quan el MapControl s'ha carregat completament
            LoadMap();
            UpdateMapClip();
            LoadMarkers();
        }

        private void LoadMap()
        {
            // Desactiva l'ús de SQLite per evitar errors
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            // Configura el proveïdor del mapa en mode fosc
            MapControl.MapProvider = GMapProviders.OpenStreetMap;

            // Centra el mapa en Barcelona
            MapControl.Position = new PointLatLng(41.3851, 2.1734);

            // Nivell de zoom
            MapControl.MinZoom = 5;
            MapControl.MaxZoom = 18;
            MapControl.Zoom = 14;

            // Controls d'interacció
            MapControl.CanDragMap = true;
            MapControl.DragButton = System.Windows.Input.MouseButton.Left;

            // Desactiva les etiquetes per un look més net
            MapControl.ShowTileGridLines = false;
        }

        private void MapControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateMapClip();
        }

        private void UpdateMapClip()
        {
            clipGeometry.Rect = new Rect(0, 0, MapControl.ActualWidth, MapControl.ActualHeight);
            clipGeometry.RadiusX = 20;
            clipGeometry.RadiusY = 20;
        }

        private void LoadMarkers()
        {
            MapControl.Markers.Clear();

            foreach (var marker in _viewModel.Markers)
            {
                MapControl.Markers.Add(marker);
            }
        }
    }
}