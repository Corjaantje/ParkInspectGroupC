using ParkInspectGroupC.ViewModel;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParkInspectGroupC.View.QuestionnaireModules
{
    /// <summary>
    /// Interaction logic for VehicleCountControl.xaml
    /// </summary>
    public partial class VehicleCountControl : UserControl
    {
        private QuestionnaireViewModel qvm;
        private int moduleId;

        public VehicleCountControl(int moduleId,  QuestionnaireViewModel qvm)
        {
            this.qvm = qvm;
            this.moduleId = moduleId;
            InitializeComponent();
            Loaded += GetQuestionnaireViewModelReference;
        }

        private void GetQuestionnaireViewModelReference(object sender, RoutedEventArgs routedEventArgs)
        {
            QuestionnaireModuleViewModelBase vm = DataContext as QuestionnaireModuleViewModelBase;
            vm.SetQuestionnaireViewModelReference(qvm);
            vm.SetModuleId(moduleId);
        }
    }
}
