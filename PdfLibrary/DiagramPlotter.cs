using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ParkInspectGroupC.DOMAIN;
using Report = OxyPlot.Reporting.Report;

namespace ParkInspectGroupC.Miscellaneous
{
	public class DiagramPlotter
	{

		public PlotModel GenerateDiagram(ParkInspectGroupC.DOMAIN.Report report, Diagram diagram)
		{
			PlotModel MyModel = new PlotModel();
			Diagram loadedDiagram;
			Question loadedQuestion;
			List<Questionnaire> loadedQuestionnaires;
			List<Inspection> loadedInspections;
			using (var context = new ParkInspectEntities())
			{
				loadedDiagram = (from a in context.Diagram where a.Id == diagram.Id select a).First();
				loadedQuestion = (from a in context.Question where a.Id == loadedDiagram.QuestionId select a).First();
				Assignment loadedAssignment  = (from a in context.Assignment where a.Id == report.AssignmentId select a).FirstOrDefault();
				loadedInspections = (from a in context.Inspection where a.AssignmentId == loadedAssignment.Id select a).ToList();

				//loadedAnswers = (from a in context.QuestionAnswer where a.QuestionId == loadedQuestion.Id select a).ToList();
			}

			MyModel.Title = loadedQuestion.Description;
			MyModel.Axes.Add(new DateTimeAxis() { Position = AxisPosition.Bottom, Title = "Datum", MinorIntervalType = DateTimeIntervalType.Days, StringFormat = "yyyy/MM/dd", IntervalType = DateTimeIntervalType.Days, });
			MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
			List<DataPoint> points = new List<DataPoint>();
			LineSeries serie = new LineSeries();

			foreach (var inspection in loadedInspections)
			{
				using (var context = new ParkInspectEntities())
				{
					// Load the questionnaires.
					loadedQuestionnaires = (from a in context.Questionnaire where a.InspectionId == inspection.Id select a).ToList();
					foreach (var questionaire in loadedQuestionnaires)
					{
						// Load the correct question.
						Question quest = (from a in context.Question where a.Id == loadedQuestion.Id select a).FirstOrDefault();
						// Load the answer.
						QuestionAnswer answer = (from a in context.QuestionAnswer where a.QuestionId == quest.Id && a.QuestionnaireId == questionaire.Id select a).FirstOrDefault();

						DateTime date = DateTime.Parse(answer.DateCreated.ToString());
						DataPoint datapoint = DateTimeAxis.CreateDataPoint(date, LinearAxis.ToDouble(answer.Result));
						serie.Points.Add(datapoint);
					}
				}
			}

			//foreach (var answer in loadedAnswers)
			//{
			//	DateTime date = DateTime.Parse(answer.DateCreated.ToString());
			//	DataPoint datapoint = DateTimeAxis.CreateDataPoint(date, LinearAxis.ToDouble(answer.Result));
			//	serie.Points.Add(datapoint);
			//}

			points.Add(DateTimeAxis.CreateDataPoint(DateTime.Parse("2016-12-20"), 75));

			serie.Points.AddRange(points);
			MyModel.Series.Add(serie);

			MyModel.ResetAllAxes();
			MyModel.InvalidatePlot(true);

			return MyModel;

		}
	}
}
