using GalaSoft.MvvmLight;
using ParkInspectGroupC.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkInspectGroupC.ViewModel.Results;
using LocalDatabase.Domain;

namespace ParkInspectGroupC.ViewModel
{
    public class AssignmentResultViewModel : ViewModelBase
    {

        public long SelectedAssignmentId { get { return Settings.Default.SelectedAssignmentId; } }

        public ObservableCollection<AggregatedResult> AggregatedResults;

        public AssignmentResultViewModel()
        {
            RaisePropertyChanged("SelectedAssignmentId");

            List<QuestionnaireResult> results = GetAllAssignmentResults(Settings.Default.SelectedAssignmentId);


        }

        private List<QuestionnaireResult> GetAllAssignmentResults(long assignmentId)
        {
            List<QuestionnaireResult> results = new List<QuestionnaireResult>();

            using (var context = new LocalParkInspectEntities())
            {
                // Get all inspectionID's related to the assignment
                List<long> inspectionIds = (from i in context.Inspection where i.AssignmentId == assignmentId select i.Id).ToList();

                foreach (long inspectionId in inspectionIds)
                {
                    QuestionnaireResult questionnaireResult = new QuestionnaireResult();
                    questionnaireResult.InspectionId = inspectionId;
                    questionnaireResult.DateTime = (from d in context.Inspection where d.AssignmentId == assignmentId select d.StartDate).First();

                    //questionnaireResult.Keywords = (from k in context.)
                    

                }
            }


            return results;
        }



    }
}
