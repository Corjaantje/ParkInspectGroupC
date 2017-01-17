using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class NewAssignmentViewModel : ViewModelBase
    {
        private List<Customer> allCustomers;

        public NewAssignmentViewModel()
        {
            TopLabel = " Nieuwe opdracht";
            CreatedAssignment = "";

            generateAllCustomers();

            CreateAssignment = new RelayCommand(createAssignment);
            AssignmentOverview = new RelayCommand(openAssignmentOverview);

        }

        private void openAssignmentOverview()
        {
            Navigator.SetNewView(new AssignmentOverview());
        }
        public void createAssignment()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    var assign = new Assignment();

                    assign.Id = context.Assignment.Max(u => u.Id) + 1;
                    assign.CustomerId = getCustomerId();
                    assign.ManagerId = getManager();
                    assign.Description = Description;
                    assign.DateCreated = DateTime.Today;
                    assign.DateUpdated = DateTime.Today;

                    context.Assignment.Add(assign);
                    context.SaveChanges();
                }

                TopLabel = "Opdracht aangemaakt";

                CreatedAssignment = Description;

                Description = "";

                RaisePropertyChanged();
            }
            catch
            {
                TopLabel = "Something went wrong, changes are not saved";

                RaisePropertyChanged();
            }
        }

        private void selectedCustomerChanged()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    var customer = context.Customer.Single(c => c.Id == _customerIndex + 1);

                    CustomerDescription = customer.Name + "\n" + customer.Location + "\n" + customer.Phonenumber;

                    RaisePropertyChanged("CustomerDescription");
                }
            }
            catch
            {
                CustomerDescription = "Something went wrong";
                RaisePropertyChanged("CustomerDescription");
            }
        }

        private void generateAllCustomers()
        {
            try
            {
                using (var context = new LocalParkInspectEntities())
                {
                    allCustomers = context.Customer.ToList();

                    var tempArray = new List<string>();

                    foreach (var c in allCustomers)
                        tempArray.Add(c.Name);

                    AllCustomerNames = new ObservableCollection<string>(tempArray);
                }
            }
            catch
            {
                TopLabel = "Database not working";
            }
        }


        private int getCustomerId()
        {
            try
            {
                return (int) allCustomers[_customerIndex].Id;
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);

                //will crash in the createAssignment method
                //error will be shown there
                return -1;
            }
        }

        private int getManager()
        {
            // needs to properly assign to a manager
            return 4;
        }

        #region properties

        public string Description { get; set; }

        public string TopLabel { get; set; }

        public string CreatedAssignment { get; set; }

        public ObservableCollection<string> AllCustomerNames { get; set; }

        private int _customerIndex;

        public int CustomerIndex
        {
            get { return _customerIndex; }
            set
            {
                _customerIndex = value;
                selectedCustomerChanged();
            }
        }

        public string CustomerDescription { get; set; }

        public ICommand CreateAssignment { get; set; }

        public ICommand AssignmentOverview { get; set; }

        #endregion properties
    }
}