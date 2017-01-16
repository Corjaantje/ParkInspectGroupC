using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
	class EditAssignmentViewModel
	{

		private Assignment _currentAssignment;
		public Assignment CurrentAssignment
		{
			get { return _currentAssignment; }
			set { _currentAssignment = value; }
		}

		private String _description;
		public String Description
		{
			get { return _description; }
			set { _description = value; }
		}

		AssignmentOverviewViewModel AssignmentViewModel;

		public RelayCommand FinishEdit { get; set; }

		public EditAssignmentViewModel(Assignment a, AssignmentOverviewViewModel avm)
		{
			FinishEdit = new RelayCommand(EditAssignment);

			AssignmentViewModel = avm;

			CurrentAssignment = a;
			Description = CurrentAssignment.Description;

		}

		private void EditAssignment()
		{
			CurrentAssignment.Description = Description;

			using (var context = new LocalParkInspectEntities())
			{

				var result = context.Assignment.Single(n => n.Id == _currentAssignment.Id);

				if (result != null)
				{
					result.Description = _description;
					context.SaveChanges();
				}

			}

			AssignmentViewModel.fillAllAssignments();

		}

	}
}
