using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using ParkInspectGroupC.Properties;

namespace ParkInspectGroupC.ViewModel
{
    public class AvailabilityCreationViewModel : ViewModelBase
    {
        private DateTime _date;
        private DateTime? _endTime;
        private string _eTime;
        private DateTime? _startTime;
        private string _sTime;

        public AvailabilityCreationViewModel(ObservableCollection<Availability> iAvailability)
        {
            SelectedInspector = Settings.Default.Employee;
            AvailabilityList = iAvailability;
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        public string sTime
        {
            get { return _sTime; }
            set
            {
                _sTime = value;
                RaisePropertyChanged("sTime");
            }
        }

        public string eTime
        {
            get { return _eTime; }
            set
            {
                _eTime = value;
                RaisePropertyChanged("eTime");
            }
        }

        [Column(TypeName = "DateTime2")]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }

        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged("StartTime");
            }
        }

        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged("EndTime");
            }
        }

        public ObservableCollection<Availability> AvailabilityList { get; set; }
        public Employee SelectedInspector { get; set; }
        public ICommand SaveCommand { get; set; }

        private void Save()
        {
            using (var context = new LocalParkInspectEntities())
            {
                var availability = new Availability
                {
                    EmployeeId = SelectedInspector.Id,
                    Date = Date,
                    StartTime = StartTime,
                    EndTime = EndTime
                };

                context.Availability.Add(availability);
                context.SaveChanges();
            }
            Navigator.SetNewView(new InspectorsListView());
        }

        private bool CanSave()
        {
            DateTime start;
            DateTime end;
            var format = "dd-MM-yyyy HH:mm";
            if ((Date == null) ||
                !DateTime.TryParseExact(sTime, format, null, DateTimeStyles.None, out start) ||
                !DateTime.TryParseExact(eTime, format, null, DateTimeStyles.None, out end))
                return false;
            foreach (var a in AvailabilityList)
                if (a.Date == Date)
                    return false;
            StartTime = start;
            EndTime = end;
            return true;
        }
    }
}