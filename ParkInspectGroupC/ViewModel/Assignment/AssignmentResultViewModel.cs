using GalaSoft.MvvmLight;
using ParkInspectGroupC.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkInspectGroupC.Miscellaneous;
using LocalDatabase.Domain;
using System.Diagnostics;

namespace ParkInspectGroupC.ViewModel
{
    public class AssignmentResultViewModel : ViewModelBase
    {

        public long SelectedAssignmentId { get { return Settings.Default.SelectedAssignmentId; } }

        public ObservableCollection<AggregatedResult> AggregatedResults { get; set; }

        private ResultsCollector resultsCollector = new ResultsCollector();

        public AssignmentResultViewModel()
        {
            List<QuestionnaireResult> results = resultsCollector.GetAllAssignmentResults(Settings.Default.SelectedAssignmentId);
            

            // filter on inspectionId if "SingleInspectionResultsId" Setting is set
            if (Settings.Default.SingleInspectionResultsId > -1)
            {

            }

            AggregatedResults = new ObservableCollection<AggregatedResult>(resultsCollector.QuestionnaireResultsToAggregatedResults(results));


            RaisePropertyChanged("SelectedAssignmentId");

            //test
            List<string> filter = new List<string> { "Vuil nummerbord", "Auto" };
            List<PlottedResult> plots = resultsCollector.QuestionnaireResultsToPlottedResults(results, filter);
            foreach (PlottedResult plot in plots)
            {
                Debug.WriteLine(plot.DateTime + " - " + plot.Value);
            }
        }



    }

}
