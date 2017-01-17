using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using LocalDatabase.Domain;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectorInspectionsViewModel : ViewModelBase
    {
        private string _name;

        public InspectorInspectionsViewModel(Employee selectedInspector)
        {
            Name = selectedInspector.FirstName + " " + selectedInspector.Prefix + " " + selectedInspector.SurName;
            using (var context = new LocalParkInspectEntities())
            {
                var IList =
                    context.Inspection.Include("InspectionStatus").Where(i => i.InspectorId == selectedInspector.Id);
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