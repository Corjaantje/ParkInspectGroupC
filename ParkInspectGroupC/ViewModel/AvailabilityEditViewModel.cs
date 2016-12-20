using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
    public class AvailabilityEditViewModel : ViewModelBase
    {
        private string _sTime;
        public string STime
        {
            get { return _sTime; }
            set { _sTime = value; RaisePropertyChanged("sTime"); }
        }
        private string _eTime;
        public string ETime
        {
            get { return _eTime; }
            set { _eTime = value; RaisePropertyChanged("eTime"); }
        }
        public DateTime Date { get; set; }
        public Availability SelectedAvailability { get; set; }
        public DateTime? TempStartTime { get; set; }
        public DateTime? TempEndTime { get; set; }

        public ICommand SaveCommand { get; set; }
        public AvailabilityEditViewModel(Availability selectedAvailability, ObservableCollection<Availability> iAvailability)
        {
            this.SelectedAvailability = selectedAvailability;
            Date = SelectedAvailability.Date;
            TempStartTime = SelectedAvailability.StartTime;
            TempEndTime = SelectedAvailability.EndTime;
            STime = TempStartTime.ToString();
            ETime = TempEndTime.ToString();
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        public void Save()
        {
            using(var context = new LocalParkInspectEntities())
            {
                SelectedAvailability.StartTime = TempStartTime;
                SelectedAvailability.EndTime = TempEndTime;
                context.Entry(SelectedAvailability).State = EntityState.Modified;
                context.SaveChanges();
            }
            Navigator.SetNewView(new InspectorsListView());
        }

        public bool CanSave()
        {
            DateTime start;
            DateTime end;
            string format = "dd-MM-yyyy HH:mm";
            
            if (!DateTime.TryParseExact(STime, format, null, DateTimeStyles.None, out start) || 
                !DateTime.TryParseExact(ETime, format, null, DateTimeStyles.None, out end))
            {
                return false;
            }

            TempStartTime = start;
            TempEndTime = end;
            return true;
        }
    }
}
