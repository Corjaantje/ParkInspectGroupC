using Microsoft.Practices.ServiceLocation;
using ParkInspectGroupC.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ParkInspectGroupC.View
{
    /// <summary>
    ///     Interaction logic for QuestionnaireView.xaml
    /// </summary>
    public partial class QuestionnaireView : UserControl
    {
        public QuestionnaireView()
        {
            //ServiceLocator.Current.GetInstance<QuestionnaireViewModel>().RefreshViewModel();
            InitializeComponent();
        }
    }
}