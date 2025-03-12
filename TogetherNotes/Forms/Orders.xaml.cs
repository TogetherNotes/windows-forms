using System.Windows.Controls;
using GMap.NET;
using GMap.NET.MapProviders;

namespace TogetherNotes.Forms
{
    public partial class Orders : UserControl
    {
        public Orders()
        {
            InitializeComponent();
            LoadMap();
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
            MapControl.Zoom = 12;

            // Controls d'interacció
            MapControl.CanDragMap = true;
            MapControl.DragButton = System.Windows.Input.MouseButton.Left;

            // Desactiva les etiquetes per un look més net
            MapControl.ShowTileGridLines = false;
        }
    }
}