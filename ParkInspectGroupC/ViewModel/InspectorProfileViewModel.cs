using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using LocalDatabase.Domain;
using ParkInspectGroupC.Properties;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectorProfileViewModel : ViewModelBase
    {
        private string _adress;
        private string _email;

        private IEnumerable<object> _inspections;

        private Employee _manager;

        private string _managerName;
        private string _name;

        private string _phoneNumber;

        private string _status;
        private readonly Employee Emp;

        public InspectorProfileViewModel()
        {
            Emp = Settings.Default.LoggedInEmp;
            try
            {
                Name = Emp.FirstName + " " + Emp.Prefix + " " + Emp.SurName + " (" + Emp.Gender + ")";
                Adress = Emp.Address + ", " + Emp.ZipCode + ", " + Emp.City;
                Email = Emp.Email;
                PhoneNumber = Emp.Phonenumber;
                using (var context = new LocalParkInspectEntities())
                {
                    Status =
                        (from s in context.EmployeeStatus where s.Id == Emp.EmployeeStatusId select s).FirstOrDefault()
                            .Description;

                    var aList = context.Availability.Where(a => a.EmployeeId == Emp.Id).ToList();
                    InspectorAvailability = new ObservableCollection<Availability>(aList);
                }
                try
                {
                    using (var context = new LocalParkInspectEntities())
                    {
                        Manager = (from m in context.Employee where m.Id == Emp.ManagerId select m).FirstOrDefault();
                    }

                    ManagerName = Manager.FirstName + " " + Manager.Prefix + " " + Manager.SurName;
                }
                catch
                {
                    ManagerName = "Deze medewerker heeft geen manager.";
                }
                using (var context = new LocalParkInspectEntities())
                {
                    Inspections = (from insp in context.Inspection
                        where insp.InspectorId == Emp.Id
                        select new
                        {
                            insp.Id,
                            insp.Location,
                            InspectionStatus =
                            (from inspStat in context.InspectionStatus
                                where inspStat.Id == insp.StatusId
                                select inspStat).FirstOrDefault().Description
                        }).ToList();
                }
            }
            catch
            {
                Console.WriteLine("OOPS! something went wrong pulling data from the DataBase!");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Adress
        {
            get { return _adress; }
            set
            {
                _adress = value;
                RaisePropertyChanged("Adress");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        public Employee Manager
        {
            get { return _manager; }
            set
            {
                _manager = value;
                RaisePropertyChanged("Manager");
            }
        }

        public string ManagerName
        {
            get { return _managerName; }
            set
            {
                _managerName = value;
                RaisePropertyChanged("ManagerName");
            }
        }

        public IEnumerable<object> Inspections
        {
            get { return _inspections; }
            set
            {
                _inspections = value;
                RaisePropertyChanged("Inspections");
            }
        }

        public ObservableCollection<Availability> InspectorAvailability { get; set; }
    }
}