using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
    public class MapViewModel
    {
        GMapControl gmap { get; set; }

        public MapViewModel()
        {
            gmap = new GMapControl();
            gmap.Zoom = 5;
            gmap.MaxZoom = 15;
            gmap.MinZoom = 1;
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Netherlands");
            gmap.CenterCrossPen = null;
            gmap.DragButton = MouseButton.Left;

        }
    }
}
