using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectionEditViewModel : ViewModelBase
    {
        private DateTime _eindDatum;

        private string _locatie;


        private string _selectedAssignment;

        private string _selectedInspector;

        private string _selectedRegion;

        private string _selectedStatus;

        private DateTime _startDatum;


        private readonly InspectionViewModel inspectionVM;

        public InspectionEditViewModel(InspectionViewModel inspectionViewModel)
        {
            inspectionVM = inspectionViewModel;
            Assignments = new ObservableCollection<string>();
            Inspectors = new ObservableCollection<string>();
            Stats = new ObservableCollection<string>();
            Regions = new ObservableCollection<string>();
            SaveCommand = new RelayCommand(saveInspection);

            using (var context = new LocalParkInspectEntities())
            {
                var assignments = context.Assignment.ToList();

                foreach (var assignment in assignments)
                {
                    Assignments.Add(assignment.Description);

                    if (assignment.Id == inspectionVM.SelectedInspection.AssignmentId)
                        SelectedAssignment = assignment.Description;
                }

                var regions = context.Region.ToList();

                foreach (var region in regions)
                {
                    Regions.Add(region.Region1);

                    if (region.Id == inspectionVM.SelectedInspection.RegionId)
                        SelectedRegion = region.Region1;
                }

                var stats = context.InspectionStatus.ToList();

                foreach (var stat in stats)
                {
                    Stats.Add(stat.Description);

                    if (inspectionVM.SelectedInspection.StatusId == stat.Id)
                        SelectedStatus = stat.Description;
                }

                var inspectors = context.Employee.ToList();

                foreach (var inspector in inspectors)
                {
                    if (inspector.IsInspecter)
                        Inspectors.Add(inspector.SurName);

                    if (inspector.Id == inspectionVM.SelectedInspection.InspectorId)
                        SelectedInspector = inspector.SurName;
                }


                Locatie = inspectionVM.SelectedInspection.Location;
                StartDatum = inspectionVM.SelectedInspection.StartDate;
                EindDatum = inspectionVM.SelectedInspection.EndDate;
            }
        }

        public ICommand SaveCommand { get; set; }

        public ObservableCollection<string> Assignments { get; set; }

        public string SelectedAssignment
        {
            get { return _selectedAssignment; }

            set
            {
                _selectedAssignment = value;
                RaisePropertyChanged("SelectedAssignment");
            }
        }

        public ObservableCollection<string> Regions { get; set; }

        public string SelectedRegion
        {
            get { return _selectedRegion; }

            set
            {
                _selectedRegion = value;
                RaisePropertyChanged("SelectedRegion");
            }
        }

        public string Locatie
        {
            get { return _locatie; }

            set
            {
                _locatie = value;
                RaisePropertyChanged("Location");
            }
        }

        public DateTime StartDatum
        {
            get { return _startDatum; }

            set
            {
                _startDatum = value;
                RaisePropertyChanged("Startdatum");
            }
        }

        public DateTime EindDatum
        {
            get { return _eindDatum; }

            set
            {
                _eindDatum = value;
                RaisePropertyChanged("Einddatum");
            }
        }

        public ObservableCollection<string> Stats { get; set; }

        public string SelectedStatus
        {
            get { return _selectedStatus; }

            set
            {
                _selectedStatus = value;
                RaisePropertyChanged("SelectedStatus");
            }
        }

        public ObservableCollection<string> Inspectors { get; set; }

        public string SelectedInspector
        {
            get { return _selectedInspector; }

            set
            {
                _selectedInspector = value;
                RaisePropertyChanged("SelectedInspector");
            }
        }

        public void saveInspection()
        {
            long assigmentId = 0;
            long regionId = 0;
            long statusId = 0;
            long inspectorId = 0;


            using (var context = new LocalParkInspectEntities())
            {
                var assignments = context.Assignment.ToList();
                var regions = context.Region.ToList();
                var stats = context.InspectionStatus.ToList();
                var inspectors = context.Employee.ToList();

                foreach (var assignment in assignments)
                    if (assignment.Description == SelectedAssignment)
                        assigmentId = assignment.Id;


                foreach (var region in regions)
                    if (region.Region1 == SelectedRegion)
                        regionId = region.Id;


                foreach (var stat in stats)
                    if (stat.Description == SelectedStatus)
                        statusId = stat.Id;


                foreach (var inspector in inspectors)
                    if (inspector.SurName == SelectedInspector)
                        inspectorId = inspector.Id;

                if ((assigmentId == 0) || (statusId == 0) || (regionId == 0) || (Locatie == null))
                {
                    MessageBox.Show("U heeft iets nog niet ingevuld");
                }

                else
                {
                    var inspections = context.Inspection.ToList();

                    foreach (var inspection in inspections)
                        if (inspection.Id == inspectionVM.SelectedInspection.Id)
                        {
                            inspection.AssignmentId = assigmentId;
                            inspection.InspectorId = inspectorId;
                            inspection.StatusId = statusId;
                            inspection.RegionId = regionId;
                            inspection.Location = Locatie;
                            inspection.StartDate = StartDatum;
                            inspection.EndDate = EindDatum;
                        }

                    context.SaveChanges();
                }
            }

            inspectionVM.hideAddInspection();
        }
    }
}