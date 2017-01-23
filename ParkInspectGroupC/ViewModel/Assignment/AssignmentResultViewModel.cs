using GalaSoft.MvvmLight;
using ParkInspectGroupC.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel
{
    public class AssignmentResultViewModel : ViewModelBase
    {
        public long SelectedAssignmentId { get { return Settings.Default.SelectedAssignmentId; } }

        public ObservableCollection<Tuple<string, int>> AggregatedResults;

        public AssignmentResultViewModel()
        {

        }



    }
}
