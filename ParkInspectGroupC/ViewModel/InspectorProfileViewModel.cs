using GalaSoft.MvvmLight;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectorProfileViewModel : ViewModelBase
    {
        Employee Emp;
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { _adress = value; RaisePropertyChanged("Adress"); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged("Email"); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; RaisePropertyChanged("PhoneNumber"); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; RaisePropertyChanged("Status"); }
        }

        private Employee _manager;
        public Employee Manager
        {
            get { return _manager; }
            set { _manager = value; RaisePropertyChanged("Manager"); }
        }

        private string _managerName;
        public string ManagerName
        {
            get { return _managerName; }
            set { _managerName = value; RaisePropertyChanged("ManagerName"); }
        }

        private IEnumerable<Object> _inspections;
        public IEnumerable<Object> Inspections
        {
            get { return _inspections; }
            set { _inspections = value; RaisePropertyChanged("Inspections"); }
        }

        public InspectorProfileViewModel()
        {
            Emp = Properties.Settings.Default.LoggedInEmp;
            try
            {

                Name = Emp.FirstName + " " + Emp.Prefix + " " + Emp.SurName + " (" + Emp.Gender + ")";
                Adress = Emp.Address + ", " + Emp.ZipCode + ", " + Emp.City;
                Email = Emp.Email;  
                PhoneNumber = Emp.Phonenumber;
                using (var context = new LocalParkInspectEntities())
                {
                    Status = (from s in context.EmployeeStatus where s.Id == Emp.EmployeeStatusId select s).FirstOrDefault().Description;
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

                    Inspections = (from insp in context.Inspection where insp.InspectorId == Emp.Id select new 
                    { insp.Id, insp.Location, InspectionStatus = (from inspStat in context.InspectionStatus where inspStat.Id == insp.StatusId select inspStat).FirstOrDefault().Description}).ToList();
                }
                
            }
            catch
            {
                Console.WriteLine("OOPS! something went wrong pulling data from the DataBase!");
            }
        }



    }
}

