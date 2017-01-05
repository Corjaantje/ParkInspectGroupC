using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using System;
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

        public string CustomerName
        {
            get{
                return _customername;
            }

            set{
                _customername = value;
                RaisePropertyChanged("CustomerName");
            }
        }

        public string StreetName
        {
            get
            {
                return _streetname;
            }

            set
            {
                _streetname = value;
                RaisePropertyChanged("StreetName");
            }
        }

        public string HouseNumber
        {
            get
            {
                return _housenumber;
            }

            set
            {
                _housenumber = value;
                RaisePropertyChanged("HouseNumber");
            }
        }


        public string CustomerLocation
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                RaisePropertyChanged("CustomerLocation");
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phonenumber;
            }

            set
            {
                _phonenumber = value;
                RaisePropertyChanged("PhoneNumber");
               
            }
        }


        public string CustomerMail
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

            if (string.IsNullOrWhiteSpace(CustomerName)
                || string.IsNullOrWhiteSpace(StreetName)
                || string.IsNullOrWhiteSpace(HouseNumber)
                || string.IsNullOrWhiteSpace(CustomerLocation)
                || string.IsNullOrWhiteSpace(PhoneNumber)
                || string.IsNullOrWhiteSpace(CustomerMail)) 
                {
                 return false;
            }

            return true;
        }


        public void addCustomer()
        {
            using (var context = new LocalParkInspectEntities())
            {
                var customer = new Customer()
                {
                    Name = CustomerName,
                    Address = StreetName + " " + HouseNumber,
                    Location = CustomerLocation,
                    Phonenumber = PhoneNumber,
                    Email = CustomerMail

                };

                context.Customer.Add(customer);
                context.SaveChanges();
                Console.WriteLine("TEST geslaagd");
            }
        }


        public ICommand AddCustomerCommand { get; set; }
    }
}
