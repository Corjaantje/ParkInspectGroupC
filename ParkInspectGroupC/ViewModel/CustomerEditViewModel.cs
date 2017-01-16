﻿using GalaSoft.MvvmLight;
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
            CustomerName = customerList.SelectedCustomer.Name;
            string[] array = customerList.SelectedCustomer.Address.Split(' ');
            StreetName = array[0];
            HouseNumber = array[1];
            CustomerLocation = customerList.SelectedCustomer.Location;
            PhoneNumber = customerList.SelectedCustomer.Phonenumber;
            CustomerMail = customerList.SelectedCustomer.Email;
        }

        public string CustomerName
        {
            get
            {
                return _customername;
            }

            set
            {
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
                RaisePropertyChanged("CustomerMail");
            }
        }

        private bool canEditCustomer()
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


        public void editCustomer()
        {
            using (var context = new LocalParkInspectEntities())
            {
                List<Customer> customers = context.Customer.ToList<Customer>();
                foreach(var customer in customers)
                {
                    if(customer.Id == _customerList.SelectedCustomer.Id)
                    {
                        customer.Name = CustomerName;
                        customer.Address = StreetName + " " + HouseNumber;
                        customer.Location = CustomerLocation;
                        customer.Phonenumber = PhoneNumber;
                        customer.Email = CustomerMail;
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
