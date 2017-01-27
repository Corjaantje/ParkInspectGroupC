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
        private DiagramPlotter diagramPlotter = new DiagramPlotter();

        public DiagramPreviewViewModel()
        {
            MyModel = diagramPlotter.GenerateDiagram(3, new System.Collections.Generic.List<string> { "Vuil nummerbord", "Auto" });
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