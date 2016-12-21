using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ParkInspectGroupC.ViewModel
{

    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            RegisterViewModels();

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
            SimpleIoc.Default.Register<ViewModelLocator>(); //Needed for the cleanup method ~Roy
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<ManagerDashboardViewModel>();
            SimpleIoc.Default.Register<InspectorListViewModel>();
            SimpleIoc.Default.Register<AvailabilityCreationViewModel>();
            SimpleIoc.Default.Register<AvailabilityEditViewModel>();
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
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public MapViewModel Map
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MapViewModel>();
            }
        }
        
		public LoginViewModel LoginWindow
		{
			get
			{
				return ServiceLocator.Current.GetInstance<LoginViewModel>();
			}
		}

        public CustomerCreationViewModel CustomerCreation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CustomerCreationViewModel>();
            }
        }

        public QuestionnaireViewModel QuestionnaireViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionnaireViewModel>();
            }
        }

	    public EmployeeCreationViewModel EmployeeCreation
	    {
		    get { return ServiceLocator.Current.GetInstance<EmployeeCreationViewModel>(); }
	    }

	    public InspectorProfileViewModel InspectorProfile
	    {
		    get { return new InspectorProfileViewModel(); }
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
            get
            {
                return ServiceLocator.Current.GetInstance<CustomerEditViewModel>();
            }
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

        public InspectorEditViewModel EditInspector
        {
            get { return new InspectorEditViewModel(InspectorList.SelectedInspector); }
        }
         public AvailabilityCreationViewModel Availability
        {
            get { return new AvailabilityCreationViewModel(InspectorList.SelectedInspector, InspectorList.InspectorAvailability); }
        }

        public AvailabilityEditViewModel EditAvailability
        {
            get { return new AvailabilityEditViewModel(InspectorList.SelectedAvailability,InspectorList.InspectorAvailability); }
        }

        public void Cleanup()
        {
            UnRegisterViewModels();
            Properties.Settings.Default.LoggedInEmp = null;
            RegisterViewModels();
        }
    }
}