using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using ParkInspectGroupC.ViewModel;

namespace ParkInspectGroupC.Miscellaneous
{
	public static class Navigator
	{
		private static LinkedList<UserControl> ViewHistory = new LinkedList<UserControl>();
		private static LinkedListNode<UserControl> _currentViewNode;

		private static MainViewModel MainView
		{
			get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
		}

		private static void SetView(UserControl view)
		{
			MainView.CurrentView = view;
		}

		public static void SetNewView(UserControl view)
		{
			SetView(view);
			LinkedListNode<UserControl> lln = new LinkedListNode<UserControl>(view);
			ViewHistory.AddLast(lln);
			_currentViewNode = lln;
		}
 
		public static void Back()
		{
			if (_currentViewNode != null && ViewHistory.Count > 1)
			{
				SetView(_currentViewNode.Previous.Value);
				_currentViewNode = _currentViewNode.Previous;
				ViewHistory.Remove(_currentViewNode.Next);
			}
		}

		public static bool CanGoBack()
		{
			if (_currentViewNode != null && ViewHistory.Count > 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
