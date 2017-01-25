using System.Linq;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ParkInspectGroupC.ViewModel
{
    internal class EditAssignmentViewModel : ViewModelBase
    {
        private readonly AssignmentOverviewViewModel AssignmentViewModel;
        private int _customerIndex;
        private List<Customer> allCustomers;
        public string CustomerDescription { get; set; }
        public ObservableCollection<string> AllCustomerNames { get; set; }

        public EditAssignmentViewModel(Assignment a, AssignmentOverviewViewModel avm)
        {
            CurrentAssignment = a;
            FinishEdit = new RelayCommand(EditAssignment);
            EndDate = (DateTime)CurrentAssignment.EndDate;
            Description = CurrentAssignment.Description;
            var customer = CurrentAssignment.Customer;
            CustomerDescription = customer.Name + "\n" + customer.Location + "\n" + customer.Phonenumber;

            AssignmentViewModel = avm;
            generateAllCustomers();

            
            Description = CurrentAssignment.Description;
        }

        public Assignment CurrentAssignment { get; set; }

        public string Description { get; set; }

        public RelayCommand FinishEdit { get; set; }

        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                DateTime localDate = DateTime.Today;
                if (value > localDate)
                {
                    endDate = value;
                }
                else
                {
                    endDate = DateTime.Now;
                }
            }
        }

        public int CustomerIndex
        {
            get { return _customerIndex; }
            set
            {
                _customerIndex = value;
                selectedCustomerChanged();
            }
        }

        private void generateAllCustomers()
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

        private int getCustomerId()
        {
            try
            {
                return (int)allCustomers[_customerIndex].Id;
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);

                //will crash in the editAssignment method
                //error will be shown there
                return -1;
            }
        }

        private void EditAssignment()
        {
            CurrentAssignment.Description = Description;

            using (var context = new LocalParkInspectEntities())
            {
                var result = context.Assignment.Single(n => n.Id == CurrentAssignment.Id);

                if (result != null)
                {
                    result.Description = Description;
                    result.CustomerId = getCustomerId();
                    result.EndDate = endDate;
                    result.DateUpdated = DateTime.Now;

                    result.ExistsInCentral = 2;
                    context.SaveChanges();
                }
            }

            AssignmentViewModel.fillAllAssignments();
            Navigator.Back();
        }
    }
}