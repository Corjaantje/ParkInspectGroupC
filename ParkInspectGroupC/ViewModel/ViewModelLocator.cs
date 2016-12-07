/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ParkInspectGroupC"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ParkInspectGroupC.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<CustomerCreationViewModel>();
            SimpleIoc.Default.Register<MapViewModel>();
            SimpleIoc.Default.Register<QuestionnaireViewModel>();
			SimpleIoc.Default.Register<EmployeeCreationViewModel>();
			SimpleIoc.Default.Register<InspectorProfileViewModel>();
            SimpleIoc.Default.Register<CustomerListViewModel>();
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
		    get { return ServiceLocator.Current.GetInstance<InspectorProfileViewModel>(); }
	    }

        public CustomerListViewModel CustomerList
        {
            get { return ServiceLocator.Current.GetInstance<CustomerListViewModel>(); }
        }



        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}