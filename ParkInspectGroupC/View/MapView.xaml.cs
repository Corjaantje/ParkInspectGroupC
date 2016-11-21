using GMap.NET.WindowsPresentation;
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
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        public Map()
        {
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
        private void map_MouseClick(object sender, MouseEventArgs e)
        {


            int x = (int)e.GetPosition(gmap).X - 5;
            int y = (int)e.GetPosition(gmap).Y - 5;
            var point = gmap.FromLocalToLatLng(x, y);
            Console.WriteLine(point);
            GMapMarker marker = new GMapMarker(point);
            gmap.Markers.Add(marker);
            marker.Shape = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Black,
                StrokeThickness = 1.5
            };
        }
        private Point downPoint;

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            downPoint = e.GetPosition(gmap);
        }

        private void OnMouseUp(Object sender, MouseButtonEventArgs e)
        {
            if (Math.Abs(downPoint.X - (int)e.GetPosition(gmap).X) < SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(downPoint.Y - (int)e.GetPosition(gmap).Y) < SystemParameters.MinimumVerticalDragDistance)
            {
                map_MouseClick(sender, e);
            }
        } 
    }
}
