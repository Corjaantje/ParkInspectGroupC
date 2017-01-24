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

            List<long> inspectionIds;
            List<long> questionnaireIds;

            using (var context = new LocalParkInspectEntities())
            {
                // Get all inspectionID's related to the assignment
                inspectionIds = (from insp in context.Inspection where insp.AssignmentId == assignmentId select insp.Id).ToList();

                questionnaireIds = new List<long>();
                foreach (long inspectionId in inspectionIds)
                {
                    // Get all questionnaireID's for each inspection
                    questionnaireIds.AddRange((from ques in context.Questionaire where ques.InspectionId == inspectionId select ques.Id).ToList());
                }

                foreach (long questionnaireId in questionnaireIds)
                {

                    QuestionnaireResult questionnaireResult = new QuestionnaireResult();
                    questionnaireResult.InspectionId = inspectionId;
                    questionnaireResult.DateTime = (from d in context.Inspection where d.AssignmentId == assignmentId select d.StartDate).First();

                    // Get all questionID's related to this inspection
                    List<long> questionIds = (from q)

                    //questionnaireResult.Keywords = (from k in context.)
                    

                }
            }


            return results;
        }



    }
}
