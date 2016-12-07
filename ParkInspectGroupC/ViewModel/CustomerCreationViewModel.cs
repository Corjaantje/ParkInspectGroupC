using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
   public class CustomerCreationViewModel : ViewModelBase
    {

        private string _customername;
        private string _streetname;
        private string _housenumber;
        private string _location;
        private string _phonenumber;
        private string _customermail;

        

       
        public CustomerCreationViewModel()
        {
            AddCustomerCommand = new RelayCommand(addCustomer, canAddCustomer);
        }

        public string Customername
        {
            get{
                return _customername;
            }

            set{
                _customername = value;
                RaisePropertyChanged("Customername");
            }
        }

        public string Streetname
        {
            get
            {
                return _streetname;
            }

            set
            {
                _streetname = value;
                RaisePropertyChanged("Streetname");
            }
        }

        public string Housenumber
        {
            get
            {
                return _housenumber;
            }

            set
            {
                _housenumber = value;
                RaisePropertyChanged("Housenumber");
            }
        }


        public string Customerlocation
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                RaisePropertyChanged("Customerlocation");
            }
        }

        public string Phonenumber
        {
            get
            {
                return _phonenumber;
            }

            set
            {
                _phonenumber = value;
                RaisePropertyChanged("Phonenumber");
               
            }
        }


        public string Customermail
        {
            get
            {
                return _customermail;
            }

            set
            {
                _customermail = value;
                RaisePropertyChanged("Customermail");
            }
        }

        private bool canAddCustomer()
        {

            if (string.IsNullOrWhiteSpace(Customername)
                || string.IsNullOrWhiteSpace(Streetname)
                || string.IsNullOrWhiteSpace(Housenumber)
                || string.IsNullOrWhiteSpace(Customerlocation)
                || string.IsNullOrWhiteSpace(Phonenumber)
                || string.IsNullOrWhiteSpace(Customermail)) 
                {
                 return false;
            }

            return true;
        }


        public void addCustomer()
        {
            using (var context = new ParkInspectEntities())
            {
                var customer = new Customer()
                {
                    Name = _customername,
                    Address = _streetname + " " + _housenumber,
                    Location = _location,
                    Phonenumber = _phonenumber,
                    Email = _customermail

                };

                context.Customers.Add(customer);
                context.SaveChanges();
                Console.WriteLine("TEST geslaagd");
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //void RaisePropertyChanged(string prop)
        //{
        //    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        //}

        public ICommand AddCustomerCommand { get; set; }
    }
}
