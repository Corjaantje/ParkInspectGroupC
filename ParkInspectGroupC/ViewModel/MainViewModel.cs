using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using LocalDatabase.Domain;
using Microsoft.Practices.ServiceLocation;
using ParkInspectGroupC.Factory;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.View;
using Simple.Wpf.Themes;

namespace ParkInspectGroupC.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl _currentView;

        private bool _loggedInEmpIsManager;
        private bool _navVis;

        private Theme _selectedTheme;

        //public IEnumerable<Theme> Themes { get; private set; }
        private readonly ThemeFactory _themeFactory;

        public MainViewModel()
        {
            loggedInEmp = Settings.Default.LoggedInEmp;
            BackCommand = new RelayCommand(PerformBack, CanPerformBack);
            AssignmentNavigationCommand = new RelayCommand(PerformAssignmentNavigation);
            ProfileNavigationCommand = new RelayCommand(PerfromProfilenNavigation);
            ProfileListNavigationCommand = new RelayCommand(PerfromProfileListNavigation);
            CustomerNavigationCommand = new RelayCommand(PerformCustomerNavigation);
            HomeCommand = new RelayCommand(PerformHome);
            LogOutCommand = new RelayCommand(PerformLogOut);
            var lView = new LoginView();
            Navigator.ViewHistory.AddFirst(lView);
            Navigator._currentViewNode = new LinkedListNode<UserControl>(lView);
            CurrentView = lView;

            //List<Theme> Themes = new List<Theme>
            //{
            //	new Theme("No theme (default)", null),
            //	new Theme("Expression Dark", new Uri("/ParkInspectGroupC;component/Themes/ExpressionDark.xaml", UriKind.Relative)),
            //	new Theme("Expression Light", new Uri("/ParkInspectGroupC;component/Themes/ExpressionLight.xaml", UriKind.Relative)),
            //	new Theme("Shiny Blue", new Uri("/ParkInspectGroupC;component/Themes/ShinyBlue.xaml", UriKind.Relative)),
            //	new Theme("Shiny Red", new Uri("/ParkInspectGroupC;component/Themes/ShinyRed.xaml", UriKind.Relative)),
            //};

            _themeFactory = new ThemeFactory();

            LoadedTheme = Settings.Default.CurrentThemeUri;

            if (string.IsNullOrWhiteSpace(LoadedTheme.Trim()))
                SelectedTheme = ThemeList.FirstOrDefault();
            else
                foreach (var theme in ThemeList)
                    if (string.Compare(LoadedTheme, theme.Name) == 0)
                    {
                        SelectedTheme = theme;
                        break;
                    }
        }

        public bool NavVis
        {
            get { return _navVis; }
            set
            {
                _navVis = value;
                RaisePropertyChanged("NavVis");
            }
        }

        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                NavVis = Settings.Default.LoggedInEmp != null;
                if (Settings.Default.LoggedInEmp != null)
                    loggedInEmpIsmanager = Settings.Default.LoggedInEmp.IsManager;
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        public List<Theme> ThemeList
        {
            get { return _themeFactory.Themes; }
        }

        public string LoadedTheme { get; }

        public Theme SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                _selectedTheme = value;
                SaveSettings();
                RaisePropertyChanged("SelectedTheme");
            }
        }

        public Employee loggedInEmp { get; set; }

        public bool loggedInEmpIsmanager
        {
            get { return _loggedInEmpIsManager; }

            set
            {
                _loggedInEmpIsManager = value;
                RaisePropertyChanged("LoggedInEmpIsmanager");
            }
        }

        public ICommand BackCommand { get; set; }
        public ICommand ProfileNavigationCommand { get; set; }
        public ICommand ProfileListNavigationCommand { get; set; }
        public ICommand AssignmentNavigationCommand { get; set; }
        public ICommand CustomerNavigationCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        private void SaveSettings()
        {
            Settings.Default.CurrentThemeUri = SelectedTheme.Name;
            Settings.Default.Save();
        }

        private void PerformBack()
        {
            Navigator.Back();
        }

        private void PerformHome()
        {
            if (Settings.Default.LoggedInEmp.IsManager)
                Navigator.SetNewView(new ManagerDashboardView());
            else
                Navigator.SetNewView(new DashboardView());
        }

        private void PerfromProfileListNavigation()
        {
            Navigator.SetNewView(new InspectorsListView());
        }

        private void PerfromProfilenNavigation()
        {
            Navigator.SetNewView(new InspectorProfileView());
        }

        private void PerformAssignmentNavigation()
        {
            Navigator.SetNewView(new AssignmentOverview());
        }

        private void PerformCustomerNavigation()
        {
            Navigator.SetNewView(new CustomerListView());
        }

        private void PerformLogOut()
        {
            Settings.Default.LoggedInEmp = null;
            SimpleIoc.Default.Unregister<LoginViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            Navigator.SetNewView(new LoginView());
            ServiceLocator.Current.GetInstance<ViewModelLocator>().Cleanup();
        }

        private bool CanPerformBack()
        {
            return Navigator.CanGoBack();
        }
    }
}