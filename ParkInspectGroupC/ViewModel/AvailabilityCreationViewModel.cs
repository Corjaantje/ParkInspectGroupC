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

namespace ParkInspectGroupC.ViewModel
{
    public class AvailabilityCreationViewModel : ViewModelBase
    {
        private DateTime _date;
        private DateTime? _endTime;
        private string _eTime;
        private DateTime? _startTime;
        private string _sTime;

        public AvailabilityCreationViewModel(Employee selectedInspector,
            ObservableCollection<Availability> iAvailability)
        {
            SelectedInspector = selectedInspector;
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
            TimeSpan start;
            TimeSpan end;
            if ((Date == null) ||
                !TimeSpan.TryParseExact(sTime, "g", null, TimeSpanStyles.None, out start) ||
                !TimeSpan.TryParseExact(eTime, "g", null, TimeSpanStyles.None, out end))
                return false;

            foreach (var a in AvailabilityList) { 
                if (a.Date == Date)
                    return false;
            }
            StartTime = DateTime.Now.Date + start;
            EndTime = DateTime.Now.Date + end;
            return true;
        }
    }
}