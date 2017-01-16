using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Wpf.Themes;

namespace ParkInspectGroupC.Factory
{
	class ThemeFactory
	{
		public List<Theme> Themes;

		public ThemeFactory()
		{
			Themes = new List<Theme>();
			Themes.Add(new Theme("Geen Thema (standaard)", null));
			Themes.Add(new Theme("Donker", new Uri("/ParkInspectGroupC;component/Themes/ExpressionDark.xaml", UriKind.Relative)));
			Themes.Add(new Theme("Licht", new Uri("/ParkInspectGroupC;component/Themes/ExpressionLight.xaml", UriKind.Relative)));
			Themes.Add(new Theme("Blinkend Blauw", new Uri("/ParkInspectGroupC;component/Themes/ShinyBlue.xaml", UriKind.Relative)));
			Themes.Add(new Theme("Blinkend Rood", new Uri("/ParkInspectGroupC;component/Themes/ShinyRed.xaml", UriKind.Relative)));
		}

	}
}
