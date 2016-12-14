using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //[Column(TypeName = "DateTime2")]
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

        public Employee SelectedInspector { get; set; }
        public ICommand SaveCommand { get; set; }
        public AvailabilityCreationViewModel(Employee selectedInspector)
        {
            this.SelectedInspector = selectedInspector;
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
            TimeSpan time1;
            TimeSpan time2;
            if (Date == null || !TimeSpan.TryParse(sTime, out time1) || !TimeSpan.TryParse(eTime, out time2))
            {
                return false;
            }
            try
            {
                //StartTime = TimeSpan.ParseExact(sTime, "hh:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                //EndTime = TimeSpan.ParseExact(eTime, "hh:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
            return true;
        }
    }
}
