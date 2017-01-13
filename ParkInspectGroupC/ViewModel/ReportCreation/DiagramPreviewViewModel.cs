using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public PlotModel MyModel
		{
			get { return _myModel; }
			set { _myModel = value; RaisePropertyChanged("MyModel"); }
		}

		public DiagramPreviewViewModel(ParkInspectGroupC.DOMAIN.Report report, Diagram diagram)
		{
			MyModel = ServiceLocator.Current.GetInstance<DiagramPlotter>().GenerateDiagram(report, diagram);
			//this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
		}
	}
}
