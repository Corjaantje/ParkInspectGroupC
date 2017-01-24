using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using OxyPlot;
using ParkInspectGroupC.DOMAIN;
using ParkInspectGroupC.Miscellaneous;

namespace ParkInspectGroupC.ViewModel.ReportCreation
{
    public class DiagramPreviewViewModel : ViewModelBase
    {
        private PlotModel _myModel;

        public DiagramPreviewViewModel(Report report, Diagram diagram)
        {
            MyModel = ServiceLocator.Current.GetInstance<DiagramPlotter>().GenerateDiagram(report, diagram);
            //this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

        public PlotModel MyModel
        {
            get { return _myModel; }
            set
            {
                _myModel = value;
                RaisePropertyChanged("MyModel");
            }
        }
    }
}