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

        
        public MapViewModel(IEnumerable<Inspection> PInspections)
        {
            
            AddInspections(PInspections);
        }

        private void AddInspections(IEnumerable<Inspection> PInspections)
        {

            int frequency;
            double Lat;
            double Long;
            Coordinate coord = null;
            Markers = new List<GMapMarker>();
            try
            {
                foreach (var insp in PInspections)
                {
                    frequency = 0;
                    Lat = 0;
                    Long = 0;
                    using (var context = new ParkInspectEntities())
                    {
                       
                        coord = (from c in context.Coordinates
                                 where c.InspectionId == insp.Id
                                 select c).FirstOrDefault();
                        frequency = (from c in context.Coordinates
                                     where c.Latitude == coord.Latitude && c.Longitude == coord.Longitude
                                     select c).Count();
                        Lat = (from c in context.Coordinates
                               where c.InspectionId == insp.Id
                               select c).FirstOrDefault().Latitude;
                        Long = (from c in context.Coordinates
                                where c.InspectionId == insp.Id
                                select c).FirstOrDefault().Longitude;
                    }
                    var point = new GMap.NET.PointLatLng(Lat, Long);
                    GMapMarker marker = new GMapMarker(point);
                    marker.Shape = new Ellipse
                    {
                        Width = frequency *10,
                        Height = frequency *10,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1.5
                    };
                    Markers = Markers.Concat(new[] { marker });
                }
            }

            catch(Exception){}
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
