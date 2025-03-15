using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using GMap.NET;
using GMap.NET.MapProviders;

namespace TogetherNotes.Forms
{
    public partial class Map : UserControl
    {
        private readonly RectangleGeometry clipGeometry;

        public Map()
        {
            InitializeComponent();

            // Inicialitzem el Clip
            clipGeometry = new RectangleGeometry();
            MapControl.Clip = clipGeometry;

            // Asegurem que el MapControl es carrega abans d'executar la configuració
            MapControl.Loaded += MapControl_Loaded;
            MapControl.SizeChanged += MapControl_SizeChanged;
        }

        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Carreguem i configurem el mapa només quan el MapControl s'ha carregat completament
            LoadMap();

            // Establim el Clip inicial
            UpdateMapClip();
        }

        private void LoadMap()
        {
            // Configura el proveïdor del mapa en mode fosc
            MapControl.MapProvider = GMapProviders.OpenStreetMap;

            // Activa el mode només servidor (evita errors)
            GMaps.Instance.Mode = AccessMode.ServerOnly;

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
            // Actualitzem el Clip quan el mapa canviï de mida
            UpdateMapClip();
        }

        private void UpdateMapClip()
        {
            clipGeometry.Rect = new Rect(0, 0, MapControl.ActualWidth, MapControl.ActualHeight);
            clipGeometry.RadiusX = 20;
            clipGeometry.RadiusY = 20;
        }
    }
}