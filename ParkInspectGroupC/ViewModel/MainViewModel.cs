using System;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using ParkInspectGroupC.Miscellaneous;
using Simple.Wpf.Themes;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using GalaSoft.MvvmLight.CommandWpf;	
using ParkInspectGroupC.Properties;

namespace ParkInspectGroupC.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
		public IEnumerable<Theme> Themes { get; private set; }

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

		public ICommand SaveCommand { get; set; }

		public MainViewModel()
		{
			SaveCommand = new RelayCommand(SaveSettings);


			Themes = new List<Theme>
			{
				new Theme("No theme (default)", null),
				new Theme("Expression Dark", new Uri("/ParkInspectGroupC;component/Themes/ExpressionDark.xaml", UriKind.Relative)),
				new Theme("Expression Light", new Uri("/ParkInspectGroupC;component/Themes/ExpressionLight.xaml", UriKind.Relative)),
				new Theme("Shiny Blue", new Uri("/ParkInspectGroupC;component/Themes/ShinyBlue.xaml", UriKind.Relative)),
				new Theme("Shiny Red", new Uri("/ParkInspectGroupC;component/Themes/ShinyRed.xaml", UriKind.Relative)),
			};

			LoadedTheme = Settings.Default.CurrentThemeUri;

			if (string.IsNullOrWhiteSpace(_loadedTheme.Trim()))
			{
				SelectedTheme = Themes.FirstOrDefault();
			}
			else
			{
				foreach (var theme in Themes)
				{
					if (string.Compare(_loadedTheme, theme.Name) == 0)
					{
						SelectedTheme = theme;
						break;
					}		
				}
			}
		}

    }
}