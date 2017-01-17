using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class CustomerListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Customer> _allCustomers;
        private ObservableCollection<Customer> _customers;
        private string _searchString = "Search";

        public CustomerListViewModel()
        {
            var searchedCustomers = new ObservableCollection<Customer>();

            using (var context = new LocalParkInspectEntities())
            {
                var customers = context.Customer.ToList();


                foreach (var customer in customers)
                    searchedCustomers.Add(customer);
            }

            _allCustomers = searchedCustomers;
            _customers = searchedCustomers;
            DeleteCustomerCommand = new RelayCommand(deleteCustomer, canDelete);
            AddCustomerCommand = new RelayCommand(openAddWindow);
            EditCustomerCommand = new RelayCommand(openEditWindow);
        }

        public ICommand DeleteCustomerCommand { get; set; }
        public ICommand AddCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }

            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }


        public Customer SelectedCustomer { get; set; }

        public string SearchString
        {
            get { return _searchString; }

            set
            {
                _searchString = value;


                var searchedCustomers = new ObservableCollection<Customer>();

                foreach (var customer in _allCustomers)
                    if ((SearchString != null) && customer.Name.ToLower().Contains(SearchString.ToLower()))
                        searchedCustomers.Add(customer);


                _customers = searchedCustomers;


                RaisePropertyChanged("Customers");
            }
        }

        public void deleteCustomer()
        {
            using (var context = new LocalParkInspectEntities())
            {
                var customers = context.Customer.ToList();
                if (SelectedCustomer != null)
                {
                    foreach (var customer in customers)
                        if (customer.Id == SelectedCustomer.Id)
                            context.Customer.Remove(customer);
                    context.SaveChanges();
                }
            }

            _customers.Remove(SelectedCustomer);
            _allCustomers.Remove(SelectedCustomer);
            RaisePropertyChanged("Customers");
        }

        public bool canDelete()
        {
            return true;
        }

        public void openAddWindow()
        {
            Navigator.SetNewView(new CustomerCreationView());
        }

        public void openEditWindow()
        {
            if (SelectedCustomer != null)
                Navigator.SetNewView(new CustomerEditView());

            else
                MessageBox.Show("Selecteer alstublieft eerst een klant");
        }
    }
}