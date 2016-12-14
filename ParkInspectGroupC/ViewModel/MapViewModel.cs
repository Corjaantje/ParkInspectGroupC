using GMap.NET.WindowsPresentation;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ParkInspectGroupC.ViewModel
{
    public class MapViewModel
    {
        public ICommand ShowInspectionsCommand { get; set; }
        public IEnumerable<GMapMarker> Markers { get; set; }

        //This is the actual constructor
        //public MapViewModel(IEnumerable<Inspection> PInspections)
        //{  AddInspections(PInspections);
        //}

        //This is a debug constructor!
        public MapViewModel()
        {
            Debug.WriteLine("Open screen");
            IEnumerable<Inspection> PInspections = null;
            using (var context = new LocalParkInspectEntities())
            {
                PInspections = (from c in context.Inspection
                    select c).ToList();
            }
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
                    using (var context = new LocalParkInspectEntities())
                    {
                       
                        coord = (from c in context.Coordinate
                                 where c.InspectionId == insp.Id
                                 select c).FirstOrDefault();
                        frequency = (from c in context.Coordinate
                                     where c.Latitude == coord.Latitude && c.Longitude == coord.Longitude
                                     select c).Count();
                        Lat = (from c in context.Coordinate
                               where c.InspectionId == insp.Id
                               select c).FirstOrDefault().Latitude;
                        Long = (from c in context.Coordinate
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
