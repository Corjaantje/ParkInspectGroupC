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

        private string Customername;
        private string Streetname;
        private string Housenumber;
        private string Location;
        private string Phonenumber;
        private string Customermail;

        

       
        public CustomerCreationViewModel()
        {
            AddCustomerCommand = new RelayCommand(addCustomer, canAddCustomer);
        }

        public string customerName
        {
            get{
                return Customername;
            }

            set{
                Customername = value;
                RaisePropertyChanged("customerName");
            }
        }

        public string streetName
        {
            get
            {
                return Streetname;
            }

            set
            {
                Streetname = value;
                RaisePropertyChanged("streetName");
            }
        }

        public string houseNumber
        {
            get
            {
                return Housenumber;
            }

            set
            {
                Housenumber = value;
                RaisePropertyChanged("houseNumber");
            }
        }


        public string customerLocation
        {
            get
            {
                return Location;
            }

            set
            {
                Location = value;
                RaisePropertyChanged("customerLocation");
            }
        }

        public string phoneNumber
        {
            get
            {
                return Phonenumber;
            }

            set
            {
                Phonenumber = value;
                RaisePropertyChanged("phoneNumber");
               
            }
        }


        public string customerMail
        {
            get
            {
                return Customermail;
            }

            set
            {
                Customermail = value;
                RaisePropertyChanged("customerMail");
            }
        }

        private bool canAddCustomer()
        {

            if (string.IsNullOrWhiteSpace(customerName))
                //|| string.IsNullOrWhiteSpace(streetName)
                //|| string.IsNullOrWhiteSpace(houseNumber)
                //|| string.IsNullOrWhiteSpace(customerLocation)
                //|| string.IsNullOrWhiteSpace(phoneNumber)
                //|| string.IsNullOrWhiteSpace(customerMail)) 
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
                    Name = Customername,
                    Address = Streetname + " " + Housenumber,
                    Location = Location,
                    Phonenumber = Phonenumber,
                    Email = Customermail

                };

                context.Customer.Add(customer);
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
