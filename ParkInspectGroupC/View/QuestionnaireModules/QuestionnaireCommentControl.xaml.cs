using System.Windows;
using System.Windows.Controls;
using ParkInspectGroupC.ViewModel;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;

namespace ParkInspectGroupC.View.QuestionnaireModules
{
    /// <summary>
    ///     Interaction logic for QuestionnaireCommentControl.xaml
    /// </summary>
    public partial class QuestionnaireCommentControl : UserControl
    {
        private readonly int moduleId;
        private readonly QuestionnaireViewModel qvm;

        public QuestionnaireCommentControl(int moduleId, QuestionnaireViewModel qvm)
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