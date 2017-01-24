using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels;
using ParkInspectGroupC.ViewModel.ReportCreation;

namespace ParkInspectGroupC.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            RegisterViewModels();
        }

        public ReportViewModel Reports
        {
            get { return ServiceLocator.Current.GetInstance<ReportViewModel>(); }
        }

        public DiagramPreviewViewModel DiagramPreview
        {
            get { return ServiceLocator.Current.GetInstance<DiagramPreviewViewModel>(); }
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public MapViewModel Map
        {
            get { return ServiceLocator.Current.GetInstance<MapViewModel>(); }
        }

        public LoginViewModel LoginWindow
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public CustomerCreationViewModel CustomerCreation
        {
            get { return ServiceLocator.Current.GetInstance<CustomerCreationViewModel>(); }
        }

        public EmployeeCreationViewModel EmployeeCreation
        {
            get { return ServiceLocator.Current.GetInstance<EmployeeCreationViewModel>(); }
        }

        public InspectorProfileViewModel InspectorProfile
        {
            get { return ServiceLocator.Current.GetInstance<InspectorProfileViewModel>(); }
        }

        public DatabaseSyncViewModel DatabaseSync
        {
            get { return ServiceLocator.Current.GetInstance<DatabaseSyncViewModel>(); }
        }

        public OnOffIndicatorViewModel OnOffIndicator
        {
            get { return ServiceLocator.Current.GetInstance<OnOffIndicatorViewModel>(); }
        }

        public CustomerListViewModel CustomerList
        {
            get { return ServiceLocator.Current.GetInstance<CustomerListViewModel>(); }
        }

        public CustomerEditViewModel CustomerEdit
        {
            get { return ServiceLocator.Current.GetInstance<CustomerEditViewModel>(); }
        }

        public DashboardViewModel Dashboard
        {
            get { return ServiceLocator.Current.GetInstance<DashboardViewModel>(); }
        }

        public ManagerDashboardViewModel ManagerDashboard
        {
            get { return ServiceLocator.Current.GetInstance<ManagerDashboardViewModel>(); }
        }

        public InspectorListViewModel InspectorList
        {
            get { return ServiceLocator.Current.GetInstance<InspectorListViewModel>(); }
        }


        public InspectionViewModel InspectionOverView
        {
            get { return ServiceLocator.Current.GetInstance<InspectionViewModel>(); }
        }

        public AssignmentToInspectionViewModel AssignmentToInspection
        {
            get { return ServiceLocator.Current.GetInstance<AssignmentToInspectionViewModel>(); }
        }

        public AssignmentOverviewViewModel OpdrachtOverview
        {
            get { return ServiceLocator.Current.GetInstance<AssignmentOverviewViewModel>(); }
        }
        public NewAssignmentViewModel NewAssignment
        {
            get { return ServiceLocator.Current.GetInstance<NewAssignmentViewModel>(); }
        }

        //ViewModels without ServiceLocator
        public InspectionCreationViewModel AddInspection
        {
            get { return new InspectionCreationViewModel(InspectionOverView); }
        }

        public InspectionEditViewModel EditInspection
        {
            get { return new InspectionEditViewModel(InspectionOverView); }
        }

        public InspectorInspectionsViewModel InspectorInspections
        {
            get { return new InspectorInspectionsViewModel(); }
        }

        public InspectorEditViewModel EditInspector
        {
            get { return new InspectorEditViewModel(); }
        }

        public QuestionnaireViewModel QuestionnaireViewModel
        {
            get { return new QuestionnaireViewModel(); }
        }

        public AvailabilityCreationViewModel Availability
        {
            get
            {
                return new AvailabilityCreationViewModel(InspectorList.InspectorAvailability);
            }
        }


        public AvailabilityEditViewModel EditAvailability
        {
            get
            {
                return new AvailabilityEditViewModel(InspectorList.InspectorAvailability);
            }
        }

        public AssignmentResultViewModel AssignmentResultViewModel
        {
            get { return new AssignmentResultViewModel(); }
        }

        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<CustomerCreationViewModel>();
            SimpleIoc.Default.Register<MapViewModel>();
            SimpleIoc.Default.Register<QuestionnaireViewModel>();
            SimpleIoc.Default.Register<EmployeeCreationViewModel>();
            SimpleIoc.Default.Register<InspectorProfileViewModel>();
            SimpleIoc.Default.Register<DatabaseSyncViewModel>();
            SimpleIoc.Default.Register<CustomerListViewModel>();
            SimpleIoc.Default.Register<CustomerEditViewModel>();
            SimpleIoc.Default.Register<OnOffIndicatorViewModel>();
            SimpleIoc.Default.Register<ViewModelLocator>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<ManagerDashboardViewModel>();
            SimpleIoc.Default.Register<InspectorListViewModel>();
            SimpleIoc.Default.Register<AvailabilityCreationViewModel>();
            SimpleIoc.Default.Register<AvailabilityEditViewModel>();
            SimpleIoc.Default.Register<InspectionViewModel>();
            SimpleIoc.Default.Register<AssignmentToInspectionViewModel>();
            SimpleIoc.Default.Register<InspectionCreationViewModel>();
            SimpleIoc.Default.Register<InspectionEditViewModel>();
            SimpleIoc.Default.Register<InspectorInspectionsViewModel>();
            SimpleIoc.Default.Register<AssignmentOverviewViewModel>();
            SimpleIoc.Default.Register<NewAssignmentViewModel>();
            SimpleIoc.Default.Register<InspectorProfileViewModel>();
            SimpleIoc.Default.Register<DiagramPreviewViewModel>();
            SimpleIoc.Default.Register<ReportViewModel>();

            RegisterQuestionnaireModuleViewModels();
        }

        private static void UnRegisterViewModels()
        {
            SimpleIoc.Default.Unregister<CustomerCreationViewModel>();
            SimpleIoc.Default.Unregister<MapViewModel>();
            SimpleIoc.Default.Unregister<QuestionnaireViewModel>();
            SimpleIoc.Default.Unregister<EmployeeCreationViewModel>();
            SimpleIoc.Default.Unregister<InspectorProfileViewModel>();
            SimpleIoc.Default.Unregister<DatabaseSyncViewModel>();
            SimpleIoc.Default.Unregister<CustomerListViewModel>();
            SimpleIoc.Default.Unregister<CustomerEditViewModel>();
            SimpleIoc.Default.Unregister<OnOffIndicatorViewModel>();
            SimpleIoc.Default.Unregister<DashboardViewModel>();
            SimpleIoc.Default.Unregister<ManagerDashboardViewModel>();
            SimpleIoc.Default.Unregister<InspectorListViewModel>();
            SimpleIoc.Default.Unregister<AvailabilityCreationViewModel>();
            SimpleIoc.Default.Unregister<AvailabilityEditViewModel>();
            SimpleIoc.Default.Unregister<InspectionViewModel>();
            SimpleIoc.Default.Unregister<AssignmentToInspectionViewModel>();
            SimpleIoc.Default.Unregister<InspectionCreationViewModel>();
            SimpleIoc.Default.Unregister<InspectionEditViewModel>();
            SimpleIoc.Default.Unregister<InspectorInspectionsViewModel>();
            SimpleIoc.Default.Unregister<AssignmentOverviewViewModel>();
            SimpleIoc.Default.Unregister<NewAssignmentViewModel>();
            SimpleIoc.Default.Unregister<InspectorProfileViewModel>();
            SimpleIoc.Default.Unregister<DiagramPreviewViewModel>();
            SimpleIoc.Default.Unregister<ReportViewModel>();
        }

        public void Cleanup()
        {
            UnRegisterViewModels();
            RegisterViewModels();
        }


        /* The following are ViewModels used for UserControls in the QuestionnaireView (QuestionnaireModules)
        * As one needs to be added per QuestionnaireModule, this will eventually clutter up the ViewModelLocator
        * These ViewModels will be centralised elsewhere at a later point in time, but need to be accessed through the VMC until then.
        * THESE CANNOT be used as normal views.
        * */

        #region QuestionnaireModules

        private static void RegisterQuestionnaireModuleViewModels()
        {
            SimpleIoc.Default.Register<VehicleCountControlVM>();
            SimpleIoc.Default.Register<QuestionnaireCommentControlVM>();
        }

        public VehicleCountControlVM VehicleCountControlVM
        {
            get { return new VehicleCountControlVM(); }
        }

        public QuestionnaireCommentControlVM QuestionnaireCommentControlVM
        {
            get { return new QuestionnaireCommentControlVM(); }
        }

        #endregion
    }
}