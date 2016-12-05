using GMap.NET;
using GMap.NET.ObjectModel;
using GMap.NET.WindowsPresentation;
using Microsoft.Practices.ServiceLocation;
using ParkInspectGroupC.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        MapViewModel vm;
        public MapView()
        {
            
         this.InitializeComponent();
            Loaded += Window_Loaded;
        }

       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gmap.SetPositionByKeywords("Netherlands");
            gmap.CenterCrossPen = null;
            gmap.DragButton = MouseButton.Left;
            vm = (MapViewModel)DataContext;
            Add_Markers();
        }


        private void Add_Markers()
        {
            foreach (GMapMarker marker in vm.Markers)
            {
                gmap.Markers.Add(marker);
            }

        }
    }
}
