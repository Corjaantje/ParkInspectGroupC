using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ParkInspectGroupC.View
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : Window
    {
        public MapView()
        {
            Loaded += Window_Loaded;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.SetPositionByKeywords("Netherlands");
            gmap.CenterCrossPen = null;
            gmap.DragButton = MouseButton.Left;

        }
    }
}
