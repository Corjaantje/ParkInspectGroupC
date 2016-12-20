using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
   public class AvailabilityCreationViewModel: ViewModelBase
    {
        private string _sTime;
        public string sTime
        {
            get { return _sTime; }
            set { _sTime = value; RaisePropertyChanged("sTime"); }
        }
        private string _eTime;
        public string eTime
        {
            get { return _eTime; }
            set { _eTime = value; RaisePropertyChanged("eTime"); }
        }
        private DateTime _date;
        [Column(TypeName = "DateTime")]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; RaisePropertyChanged("Date"); }
        }
        private DateTime? _startTime;
        public DateTime? StartTime
        {
            get { return _startTime; }
            set { _startTime = value; RaisePropertyChanged("StartTime"); }
        }
        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; RaisePropertyChanged("EndTime"); }
        }
        
        public ObservableCollection<Availability> AvailibilityList { get; set; }
        public Employee SelectedInspector { get; set; }
        public ICommand SaveCommand { get; set; }
        public AvailabilityCreationViewModel(Employee selectedInspector, ObservableCollection<Availability> iAvailability)
        {
            this.SelectedInspector = selectedInspector;
            this.AvailibilityList = iAvailability;
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        private void Save()
        {
            using (var context = new LocalParkInspectEntities())
            {
                var availability = new Availability
                {
                    EmployeeId = SelectedInspector.Id,
                    Date = this.Date,
                    StartTime = this.StartTime,
                    EndTime = this.EndTime
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
            string format = "dd-MM-yyyy HH:mm";
            if (Date == null || 
                !DateTime.TryParseExact(sTime, format, null, DateTimeStyles.None, out start) || 
                !DateTime.TryParseExact(eTime, format, null, DateTimeStyles.None, out end))
            {
                return false;
            }
            
            // cheeck if date already exist 
            foreach(var a in AvailibilityList)
            {
                if(a.Date == this.Date)
                {
                    return false;
                }
            }
            StartTime = start;
            EndTime = end;
            return true;
        }
    }
}
