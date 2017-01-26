using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalDatabase;
using System.Diagnostics;
using LocalDatabase.Domain;

namespace ParkInspectGroupC.Miscellaneous
{
    public class ResultsCollector
    {
        public List<QuestionnaireResult> GetAllAssignmentResults(long assignmentId)
        {
            List<QuestionnaireResult> results = new List<QuestionnaireResult>();

            List<long> inspectionIds;
            List<Questionaire> questionnaires;
            List<QuestionAnswer> questionanswers;

            using (var context = new LocalParkInspectEntities())
            {
                // Get all inspectionID's related to the assignment
                inspectionIds = (from insp in context.Inspection where insp.AssignmentId == assignmentId select insp.Id).ToList();

                questionnaires = new List<Questionaire>();
                foreach (long inspectionId in inspectionIds)
                {
                    // Get all questionnaire's for each inspection
                    questionnaires.AddRange((from ques in context.Questionaire where ques.InspectionId == inspectionId select ques).ToList());
                }

                questionanswers = new List<QuestionAnswer>();
                foreach (Questionaire questionnaire in questionnaires)
                {
                    // Get all questionanswers of each questionnaire
                    questionanswers.AddRange((from answ in context.QuestionAnswer where answ.QuestionnaireId == questionnaire.Id select answ).ToList());
                }

                foreach (QuestionAnswer questionanswer in questionanswers)
                {

                    QuestionnaireResult questionnaireResult = new QuestionnaireResult();
                    questionnaireResult.InspectionId = (from ques in questionnaires where ques.Id == questionanswer.QuestionnaireId select ques.InspectionId).First();
                    questionnaireResult.DateTime = (from insp in context.Inspection where insp.Id == questionnaireResult.InspectionId select insp.StartDate).First();
                    // TODO: seperate function to check the questionanswers actual type
                    questionnaireResult.Value = Convert.ToInt32(questionanswer.Result);

                    // Get all keywords belonging to this value
                    List<string> keywords = new List<string>();
                    List<long> keywordIds = (from qkey in context.QuestionKeyword where qkey.QuestionId == questionanswer.QuestionId select qkey.KeywordId).ToList();
                    foreach (long keywordId in keywordIds)
                    {
                        keywords.Add((from keyw in context.Keyword where keyw.Id == keywordId select keyw.Description).First());
                    }
                    questionnaireResult.Keywords = keywords;

                    results.Add(questionnaireResult);
                }
            }

            return results;
        }

        public List<AggregatedResult> QuestionnaireResultsToAggregatedResults(List<QuestionnaireResult> list, long inspectionFilter = -1)
        {
            List<AggregatedResult> results = new List<AggregatedResult>();

            if (inspectionFilter != -1) // also filter by inspection
            {
                foreach (QuestionnaireResult questionnaireresult in list)
                {
                    if (questionnaireresult.InspectionId == inspectionFilter)
                    {
                        AggregatedResult result = new AggregatedResult();
                        result.Value = questionnaireresult.Value;
                        foreach (string keyword in questionnaireresult.Keywords)
                        {
                            if (result.Keywords == null)
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
                }
            }
            else // show all results
            {
                foreach (QuestionnaireResult questionnaireresult in list)
                {
                    AggregatedResult result = new AggregatedResult();
                    result.Value = questionnaireresult.Value;
                    foreach (string keyword in questionnaireresult.Keywords)
                    {
                        if (result.Keywords == null)
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
            }
            return results;
        }

        public List<PlottedResult> QuestionnaireResultsToPlottedResults(List<QuestionnaireResult> list, List<string> keywordfilter)
        {
            List<PlottedResult> results = new List<PlottedResult>();

            foreach (QuestionnaireResult questionnaireresult in list)
            {
                // compare with filter
                if (questionnaireresult.Keywords.Count != keywordfilter.Count) continue;
                if (ContainsAll(questionnaireresult.Keywords, keywordfilter) == false) continue;


                // add the plot
                PlottedResult plottedresult = new PlottedResult();
                plottedresult.DateTime = questionnaireresult.DateTime;
                plottedresult.Value = questionnaireresult.Value;
                results.Add(plottedresult);
            }

            results.OrderByDescending(r => r.DateTime);

            return results;
        }

        private bool ContainsAll(List<string> container, List<string> contained)
        {
            foreach (string str in contained)
            {
                if (container.Contains(str) == false) return false;
            }

            return true;
        }

    }
}

