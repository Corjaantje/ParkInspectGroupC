using System;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using ParkInspectGroupC.Miscellaneous;
using Simple.Wpf.Themes;
using System.Collections.Generic;

namespace ParkInspectGroupC.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
		private Theme _selectedTheme;
		public IEnumerable<Theme> Themes { get; private set; }

		public Theme SelectedTheme
		{
			get
			{
				return _selectedTheme;
			}
			set
			{
				_selectedTheme = value;
				RaisePropertyChanged("SelectedTheme");
			}
		}

		public MainViewModel()
	    {
			Themes = new[]
			{
				new Theme("No theme (default)", null),
				new Theme("Expression Dark", new Uri("/ParkInspectGroupC;component/Themes/ExpressionDark.xaml", UriKind.Relative)),
				new Theme("Shiny Blue", new Uri("/ParkInspectGroupC;component/Themes/ShinyBlue.xaml", UriKind.Relative))
			};
		}
    }
}