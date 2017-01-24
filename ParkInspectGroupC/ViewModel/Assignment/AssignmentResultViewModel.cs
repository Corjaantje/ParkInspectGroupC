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
using System.Diagnostics;

namespace ParkInspectGroupC.ViewModel
{
    public class AssignmentResultViewModel : ViewModelBase
    {

        public long SelectedAssignmentId { get { return Settings.Default.SelectedAssignmentId; } }

        public ObservableCollection<AggregatedResult> AggregatedResults { get; set; }
         
        public AssignmentResultViewModel()
        {
            List<QuestionnaireResult> results = GetAllAssignmentResults(Settings.Default.SelectedAssignmentId);
            AggregatedResults = new ObservableCollection<AggregatedResult>(QuestionnaireResultsToAggregatedResults(results));

            RaisePropertyChanged("SelectedAssignmentId");
            RaisePropertyChanged("AggregatedResults");
            foreach (AggregatedResult aggr in AggregatedResults)
            {
                Debug.WriteLine(aggr.Keywords);
            }

        }

        private List<QuestionnaireResult> GetAllAssignmentResults(long assignmentId)
        {
            List<QuestionnaireResult> results = new List<QuestionnaireResult>();

            List<long> inspectionIds;
            List<Questionaire> questionnaires;
            List<QuestionAnswer> questionanswers;

            using (var context = new LocalParkInspectEntities())
            {
                // Get all inspectionID's related to the assignment
                inspectionIds = (from insp in context.Inspection where insp.AssignmentId == assignmentId select insp.Id).ToList();

                Debug.WriteLine("inspectieIDs:");
                questionnaires = new List<Questionaire>();
                foreach (long inspectionId in inspectionIds)
                {
                    // Get all questionnaire's for each inspection
                    questionnaires.AddRange((from ques in context.Questionaire where ques.InspectionId == inspectionId select ques).ToList());
                    Debug.WriteLine(inspectionId);
                }

                Debug.WriteLine("questionnaireIDs:");
                questionanswers = new List<QuestionAnswer>();
                foreach (Questionaire questionnaire in questionnaires)
                {
                    // Get all questionanswers of each questionnaire
                    questionanswers.AddRange((from answ in context.QuestionAnswer where answ.QuestionnaireId == questionnaire.Id select answ).ToList());
                    Debug.WriteLine(questionnaire.Id);
                }

                Debug.WriteLine("questionanswers:");
                foreach (QuestionAnswer questionanswer in questionanswers) {
                    Debug.WriteLine(questionanswer.QuestionId);

                    QuestionnaireResult questionnaireResult = new QuestionnaireResult();
                    questionnaireResult.InspectionId = (from ques in questionnaires where ques.Id == questionanswer.QuestionnaireId select ques.InspectionId).First() ;
                    questionnaireResult.DateTime = (from insp in context.Inspection where insp.AssignmentId == assignmentId select insp.StartDate).First();
                    // TODO: seperate function to check the questionanswers actual type
                    questionnaireResult.Value = Convert.ToInt32(questionanswer.Result);

                    // Get all keywords belonging to this value
                    List<string> keywords = new List<string>();
                    Debug.WriteLine("keywords:");
                    List<long> keywordIds = (from qkey in context.QuestionKeyword where qkey.QuestionId == questionanswer.QuestionId select qkey.KeywordId).ToList();
                    foreach (int keywordId in keywordIds)
                    {
                        Debug.WriteLine(keywordId);
                        keywords.Add((from keyw in context.Keyword where keyw.Id == keywordId select keyw.Description).First());
                        Debug.WriteLine((from keyw in context.Keyword where keyw.Id == keywordId select keyw.Description).First());
                    }
                    questionnaireResult.Keywords = keywords;

                    results.Add(questionnaireResult);
                }
            }

            return results;
        }

        public List<AggregatedResult> QuestionnaireResultsToAggregatedResults(List<QuestionnaireResult> list)
        {
            List<AggregatedResult> results = new List<AggregatedResult>();

            foreach(QuestionnaireResult questionnaireresult in list)
            {
                AggregatedResult result = new AggregatedResult();
                result.Value = questionnaireresult.Value;
                foreach (string keyword in questionnaireresult.Keywords) {
                    if (result.Keywords.Length < 1)
                    {
                        result.Keywords = keyword;
                    }
                    else
                    {
                        result.Keywords += (", " + keyword);
                    }
                }

                results.Add(result);
            }

            return results;
        }
    }
}
