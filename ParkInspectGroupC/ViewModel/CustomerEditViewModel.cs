using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
    public class CustomerEditViewModel
    {
        private string _customername;
        private string _streetname;
        private string _housenumber;
        private string _location;
        private string _phonenumber;
        private string _customermail;
        CustomerListViewModel _customerList;
        




        public CustomerEditViewModel(CustomerListViewModel customerList)
        {
            EditCustomerCommand = new RelayCommand(editCustomer, canEditCustomer);
            this._customerList = customerList;
            Customername = customerList.SelectedCustomer.Name;
            string[] array = customerList.SelectedCustomer.Address.Split(' ');
            Streetname = array[0];
            Housenumber = array[1];
            Customerlocation = customerList.SelectedCustomer.Location;
            Phonenumber = customerList.SelectedCustomer.Phonenumber;
            Customermail = customerList.SelectedCustomer.Email;
        }

        public string Customername
        {
            get
            {
                return _customername;
            }

            set
            {
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

        private bool canEditCustomer()
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


        public void editCustomer()
        {
            using (var context = new LocalParkInspectEntities())
            {
                List<Customer> customers = context.Customer.ToList<Customer>();
                foreach(var customer in customers)
                {
                    if(customer.Id == _customerList.SelectedCustomer.Id)
                    {
                        customer.Name = Customername;
                        customer.Address = Streetname + " " + Housenumber;
                        customer.Location = Customerlocation;
                        customer.Phonenumber = Phonenumber;
                        customer.Email = Customermail;
                    }
                }

                context.SaveChanges();
                Console.WriteLine("TEST geslaagd");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }

        public ICommand EditCustomerCommand { get; set; }
    }
}
