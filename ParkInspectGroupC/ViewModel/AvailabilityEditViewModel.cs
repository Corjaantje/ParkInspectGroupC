using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class AvailabilityEditViewModel : ViewModelBase
    {
        private string _eTime;
        private string _sTime;

        public AvailabilityEditViewModel(Availability selectedAvailability,
            ObservableCollection<Availability> iAvailability)
        {
            SelectedAvailability = selectedAvailability;
            Date = SelectedAvailability.Date;
            TempStartTime = SelectedAvailability.StartTime;
            TempEndTime = SelectedAvailability.EndTime;
            STime = TempStartTime.ToString();
            ETime = TempEndTime.ToString();
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        public string STime
        {
            get { return _sTime; }
            set
            {
                _sTime = value;
                RaisePropertyChanged("sTime");
            }
        }

        public string ETime
        {
            get { return _eTime; }
            set
            {
                _eTime = value;
                RaisePropertyChanged("eTime");
            }
        }

        public DateTime Date { get; set; }
        public Availability SelectedAvailability { get; set; }
        public DateTime? TempStartTime { get; set; }
        public DateTime? TempEndTime { get; set; }

        public ICommand SaveCommand { get; set; }

        public void Save()
        {
            using (var context = new LocalParkInspectEntities())
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
            TimeSpan start;
            TimeSpan end;
            if (!TimeSpan.TryParseExact(STime, "g", null, TimeSpanStyles.None, out start) ||
                !TimeSpan.TryParseExact(ETime, "g", null, TimeSpanStyles.None, out end))
                return false;

            TempStartTime = DateTime.Now.Date + start;
            TempEndTime = DateTime.Now.Date + end;
            return true;
        }
    }
}