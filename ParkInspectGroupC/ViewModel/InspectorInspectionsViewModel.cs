using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using LocalDatabase.Domain;
using ParkInspectGroupC.Properties;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectorInspectionsViewModel : ViewModelBase
    {
        private string _name;

        public InspectorInspectionsViewModel()
        {
            Name = Settings.Default.Employee.FirstName + " " + Settings.Default.Employee.Prefix + " " + Settings.Default.Employee.SurName;
            using (var context = new LocalParkInspectEntities())
            {
                var IList =
                    context.Inspection.Include("InspectionStatus").Where(i => i.InspectorId == Settings.Default.Employee.Id);
                Inspections = new ObservableCollection<Inspection>(IList);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public ObservableCollection<Inspection> Inspections { get; set; }
    }
}