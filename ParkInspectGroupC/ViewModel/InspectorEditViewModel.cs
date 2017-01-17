using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectorEditViewModel : ViewModelBase
    {
        private Region _selectedRegion;

        public InspectorEditViewModel(Employee selectedInspector)
        {
            SelectedInspector = selectedInspector;
            SaveCommand = new RelayCommand(SaveChanges, CanSaveChanges);

            TempInspector = new Employee();
            TempInspector.FirstName = SelectedInspector.FirstName;
            TempInspector.SurName = SelectedInspector.SurName;
            TempInspector.Prefix = SelectedInspector.Prefix;
            TempInspector.Email = SelectedInspector.Email;
            TempInspector.City = SelectedInspector.City;
            TempInspector.Address = SelectedInspector.Address;
            TempInspector.Phonenumber = SelectedInspector.Phonenumber;
            TempInspector.ZipCode = SelectedInspector.ZipCode;
            TempInspector.RegionId = SelectedInspector.RegionId;

            using (var context = new LocalParkInspectEntities())
            {
                var rList = context.Region.ToList();
                Regions = new ObservableCollection<Region>(rList);
            }
        }

        public Employee SelectedInspector { get; set; }
        public Employee TempInspector { get; set; }
        public ObservableCollection<Region> Regions { get; set; }

        public Region SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                _selectedRegion = value;
                RaisePropertyChanged("SelectedRegion");
            }
        }

        public ICommand SaveCommand { get; set; }

        private void SaveChanges()
        {
            using (var context = new LocalParkInspectEntities())
            {
                SelectedInspector.FirstName = TempInspector.FirstName;
                SelectedInspector.SurName = TempInspector.SurName;
                SelectedInspector.Prefix = TempInspector.Prefix;
                SelectedInspector.Email = TempInspector.Email;
                SelectedInspector.City = TempInspector.City;
                SelectedInspector.Address = TempInspector.Address;
                SelectedInspector.Phonenumber = TempInspector.Phonenumber;
                SelectedInspector.ZipCode = TempInspector.ZipCode;
                SelectedInspector.RegionId = TempInspector.RegionId;
                context.Entry(SelectedInspector).State = EntityState.Modified;
                context.SaveChanges();
            }

            Navigator.SetNewView(new InspectorsListView());
        }

        private bool CanSaveChanges()
        {
            if (string.IsNullOrWhiteSpace(TempInspector.City) || string.IsNullOrWhiteSpace(TempInspector.Address)
                || string.IsNullOrWhiteSpace(TempInspector.Phonenumber) ||
                string.IsNullOrWhiteSpace(TempInspector.ZipCode)
                || string.IsNullOrWhiteSpace(TempInspector.FirstName) ||
                string.IsNullOrWhiteSpace(TempInspector.SurName)
                || string.IsNullOrWhiteSpace(TempInspector.Email))
                return false;
            return true;
        }
    }
}