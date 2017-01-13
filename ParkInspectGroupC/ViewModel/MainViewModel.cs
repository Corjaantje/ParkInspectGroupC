using System.Linq;
using GalaSoft.MvvmLight;
using ParkInspectGroupC.Miscellaneous;
using Simple.Wpf.Themes;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ParkInspectGroupC.Factory;
using ParkInspectGroupC.Properties;
using ParkInspectGroupC.View;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ParkInspectGroupC.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; RaisePropertyChanged("CurrentView"); }
        }

        //public IEnumerable<Theme> Themes { get; private set; }
        private ThemeFactory _themeFactory;

        public List<Theme> ThemeList
        {
            get { return _themeFactory.Themes; }
        }

        private string _loadedTheme;
        public string LoadedTheme
        {
            get { return _loadedTheme; }
            private set { _loadedTheme = value; }
        }

        private Theme _selectedTheme;
        public Theme SelectedTheme
        {
            get
            {
                return _selectedTheme;
            }
            set
            {
                _selectedTheme = value;
                SaveSettings();
                RaisePropertyChanged("SelectedTheme");
            }
        }

        private void SaveSettings()
        {
            Settings.Default.CurrentThemeUri = SelectedTheme.Name;
            Settings.Default.Save();
        }

        public ICommand BackCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public MainViewModel()
        {
            BackCommand = new RelayCommand(PerformBack, CanPerformBack);
            LogOutCommand = new RelayCommand(PerformLogOut);
            CurrentView = new LoginView();

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

            if (string.IsNullOrWhiteSpace(_loadedTheme.Trim()))
            {
                SelectedTheme = ThemeList.FirstOrDefault();
            }
            else
            {
                foreach (var theme in ThemeList)
                {
                    if (string.Compare(_loadedTheme, theme.Name) == 0)
                    {
                        SelectedTheme = theme;
                        break;
                    }
                }
            }
        }

        private void PerformBack()
        {
            Navigator.Back();
        }

        private void PerformLogOut()
        {
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