using GalaSoft.MvvmLight.CommandWpf;
using GMap.NET.WindowsPresentation;
using ParkInspectGroupC.DOMAIN;
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
        public IEnumerable<GMapMarker> Markers { get; set; }

        int frequency;
        public MapViewModel(IEnumerable<Inspection> PInspections)
        {

            AddInspections(PInspections);
        }

        private void AddInspections(IEnumerable<Inspection> PInspections)
        {
            Markers = new List<GMapMarker>();
            foreach (var insp in PInspections)
            {
                using (var context = new ParkInspectEntities())
                {
                     frequency = (from i in context.Inspection
                                     where i.Id == insp.Id
                                     select i).Count();
                    var Lat = (from c in context.Coordinates 
                               where c.InspectionId == insp.Id 
                               select c).FirstOrDefault().Latitude;
                    var Long = (from c in context.Coordinates 
                                where c.InspectionId == insp.Id 
                                select c).FirstOrDefault().Longitude;
                }
            }
            var point = new GMap.NET.PointLatLng(Lat,Long);
            GMapMarker marker = new GMapMarker(point);
            Markers = Markers.Concat(new[] { marker });
            marker.Shape = new Ellipse
            {
                Width = frequency * 10,
                Height = 10,
                Stroke = Brushes.Black,
                StrokeThickness = 1.5
            };
            Markers = Markers.Concat(new[] { marker });
        }

        private bool CanShowInspections()
        {
            
            return true;
        }
        public IEnumerable<Object> Inspections
        {
            get;
            set; 
        }
    }
}
