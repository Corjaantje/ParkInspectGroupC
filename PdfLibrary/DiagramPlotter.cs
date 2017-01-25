using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ParkInspectGroupC.DOMAIN;

namespace ParkInspectGroupC.Miscellaneous
{
    public class DiagramPlotter
    {
        // voor testing: gebruik assignmentId = 3 en strings "Vuil nummerbord" en "Auto"
        public PlotModel GenerateDiagram(long assignmentId, List<string> keywordFilter)
        {
            ResultsCollector resultsCollector = new ResultsCollector();
            List<QuestionnaireResult> questionnaireResults = resultsCollector.GetAllAssignmentResults(assignmentId);
            List<PlottedResult> plottableData = resultsCollector.QuestionnaireResultsToPlottedResults(questionnaireResults, keywordFilter);

            var MyModel = new PlotModel();

            // TODO: geen statische titel qua filter (kan ook meer/minder sleutelwoorden zijn)
            
            MyModel.Title = "Statistische data van \"" + keywordFilter[0] + "\" + \"" + keywordFilter[1] + "\"";
            MyModel.Subtitle = "Opdracht nummer #" + assignmentId;

            MyModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Datum",
                //MinorIntervalType = DateTimeIntervalType.Days,
                IntervalType = DateTimeIntervalType.Days,
                IntervalLength = 50,
                StringFormat = "MM/dd",
                MajorGridlineStyle = LineStyle.Dash
                
            });

            MyModel.Axes.Add(new LinearAxis {Position = AxisPosition.Left});

            var points = new List<DataPoint>();
            var serie = new LineSeries();

            serie.MarkerType = MarkerType.Circle;

            foreach (PlottedResult plottedResult in plottableData)
            {
                var datapoint = DateTimeAxis.CreateDataPoint(plottedResult.DateTime.Date, Axis.ToDouble(plottedResult.Value));
                serie.Points.Add(datapoint);
            }

            MyModel.Series.Add(serie);

            MyModel.ResetAllAxes();
            MyModel.InvalidatePlot(true);

            return MyModel;
        }
    }
}