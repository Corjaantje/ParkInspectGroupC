using System.Windows;
using System.Windows.Controls;
using ParkInspectGroupC.ViewModel;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;

namespace ParkInspectGroupC.View.QuestionnaireModules
{
    /// <summary>
    ///     Interaction logic for VehicleCountControl.xaml
    /// </summary>
    public partial class VehicleCountControl : UserControl
    {
        private readonly int moduleId;
        private readonly QuestionnaireViewModel qvm;

        public VehicleCountControl(int moduleId, QuestionnaireViewModel qvm)
        {
            this.qvm = qvm;
            this.moduleId = moduleId;
            InitializeComponent();
            Loaded += GetQuestionnaireViewModelReference;
        }

        private void GetQuestionnaireViewModelReference(object sender, RoutedEventArgs routedEventArgs)
        {
            var vm = DataContext as QuestionnaireModuleViewModelBase;
            vm.SetQuestionnaireViewModelReference(qvm);
            vm.SetModuleId(moduleId);
        }
    }
}