using System.Windows;
using System.Windows.Input;
using GMap.NET;
using GMap.NET.MapProviders;
using ParkInspectGroupC.ViewModel;
using System.Windows.Controls;

namespace ParkInspectGroupC.View
{
    /// <summary>
    ///     Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        private MapViewModel vm;

        public MapView()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gmap.MapProvider = BingMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmap.SetPositionByKeywords("Netherlands");
            gmap.CenterCrossPen = null;
            gmap.DragButton = MouseButton.Left;
            vm = (MapViewModel) DataContext;
            Add_Markers();
        }


        private void Add_Markers()
        {
            foreach (var marker in vm.Markers)
                gmap.Markers.Add(marker);
        }
    }
}