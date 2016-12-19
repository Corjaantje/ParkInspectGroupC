using GalaSoft.MvvmLight;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel
{
	class NewAssignmentViewModel : ViewModelBase
	{
		private String _description;
		public String Description
		{
			get { return _description; }
			set { _description = value; }
		}

		private String _topLabel;
		public String TopLabel
		{
			get { return _topLabel;}
			set { _topLabel = value; }
		}

		private String _createdAssignment;
		public String CreatedAssignment
		{
			get { return _createdAssignment; }
			set { _createdAssignment = value; }
		}

		public NewAssignmentViewModel()
		{
			TopLabel = " Nieuwe opdracht";
			CreatedAssignment = "";
		}

		public void createAssignment()
		{

			try
			{

				using (var context = new LocalParkInspectEntities())
				{

					var assign = new Assignment();

					assign.Id = context.Assignment.Max(u => u.Id) + 1;
					assign.CustomerId = getCurrentCustomer();
					assign.ManagerId = getManager();
					assign.Description = _description;
					assign.DateCreated = DateTime.Today;
					assign.DateUpdated = DateTime.Today;

					context.Assignment.Add(assign);
					context.SaveChanges();

				}

				TopLabel = "Opdracht aangemaakt";

				CreatedAssignment = Description;

				Description = "";

				RaisePropertyChanged();
				
			}
			catch
			{

			}

		}

		private int getCurrentCustomer()
		{
			// needs to check who the current customer is
			return 5;
		}

		private int getManager()
		{
			// needs to properly assign to a manager
			return 4;
		}

	}
}
