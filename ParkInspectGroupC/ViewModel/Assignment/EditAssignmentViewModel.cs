using System.Linq;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;

namespace ParkInspectGroupC.ViewModel
{
    internal class EditAssignmentViewModel
    {
        private readonly AssignmentOverviewViewModel AssignmentViewModel;

        public EditAssignmentViewModel(Assignment a, AssignmentOverviewViewModel avm)
        {
            FinishEdit = new RelayCommand(EditAssignment);

            AssignmentViewModel = avm;

            CurrentAssignment = a;
            Description = CurrentAssignment.Description;
        }

        public Assignment CurrentAssignment { get; set; }

        public string Description { get; set; }

        public RelayCommand FinishEdit { get; set; }

        private void EditAssignment()
        {
            CurrentAssignment.Description = Description;

            using (var context = new LocalParkInspectEntities())
            {
                var result = context.Assignment.Single(n => n.Id == CurrentAssignment.Id);

                if (result != null)
                {
                    result.Description = Description;
                    context.SaveChanges();
                }
            }

            AssignmentViewModel.fillAllAssignments();
        }
    }
}