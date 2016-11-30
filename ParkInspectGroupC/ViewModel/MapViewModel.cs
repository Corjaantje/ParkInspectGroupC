using GalaSoft.MvvmLight.CommandWpf;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ParkInspectGroupC.ViewModel
{
    public class MapViewModel
    {
        public ICommand ShowInspectionsCommand { get; set; }
        public GMapControl gmap { get; set; }
        public IEnumerable<GMapMarker> markers { get; set; }

        public MapViewModel()
        {
            var point = new GMap.NET.PointLatLng(52.855864177854, 9.140625);
            Console.WriteLine(point);
            GMapMarker marker = new GMapMarker(point);
            markers = new List<GMapMarker>();
            marker.Shape = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Black,
                StrokeThickness = 1.5
            };
            markers = markers.Concat(new[] { marker });
            ShowInspectionsCommand = new RelayCommand(ShowInspections, CanShowInspections);
        }
        private void ShowInspections()
        {
           
        }

        private bool CanShowInspections()
        {
            
            return true;
        }    
    }
}
