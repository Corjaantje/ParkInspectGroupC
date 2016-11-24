using GalaSoft.MvvmLight.CommandWpf;
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
        public ICommand ShowInspectionsCommand { get; set; }
        public GMapControl gmap { get; set; }

        public MapViewModel()
        {
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
