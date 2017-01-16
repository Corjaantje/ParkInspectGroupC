using ParkInspectGroupC.ViewModel;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ParkInspectGroupC.View.QuestionnaireModules
{
    /// <summary>
    /// Interaction logic for QuestionnaireCommentControl.xaml
    /// </summary>
    public partial class QuestionnaireCommentControl : UserControl
    {
        private QuestionnaireViewModel qvm;
        private int moduleId;

        public QuestionnaireCommentControl(int moduleId, QuestionnaireViewModel qvm)
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
