using GalaSoft.MvvmLight;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel
{
    public class CustomerListViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _customers;
        private string _searchString = "Search";

        public CustomerListViewModel()
        {
            ObservableCollection<Customer> searchedCustomers = new ObservableCollection<Customer>();

            using (var context = new ParkInspectEntities())
            {
                List<Customer> customers = context.Customer.ToList();


                foreach (var customer in customers)
                {
                    //if (SearchString != null &&  customer.Name.StartsWith(SearchString))
                    //{
                    //    searchedCustomers.Add(customer);
                    //}

                    //else
                    //{
                    //    searchedCustomers.Add(customer);
                    //}
                    searchedCustomers.Add(customer);

                }
            }

            _customers = searchedCustomers;
        }
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }

            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        public string SearchString
        {
            get{
                return _searchString;
            }

            set{
                _searchString = value;
                

                ObservableCollection<Customer> searchedCustomers = new ObservableCollection<Customer>();

                using (var context = new ParkInspectEntities())
                {
                    List<Customer> customers = context.Customer.ToList();


                    foreach (var customer in customers)
                    {
                        if (SearchString != null && customer.Name.ToLower().Contains(SearchString.ToLower()))
                        {
                            searchedCustomers.Add(customer);
                        }
                    }
                }

                _customers = searchedCustomers;
                

                Console.WriteLine(_searchString);
                RaisePropertyChanged("Customers");
            }
        }
    }
}
