using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectionCreationViewModel : ViewModelBase
    {
        public ICommand SaveCommand{ get; set; }
        private InspectionViewModel inspectionVM;
        public InspectionCreationViewModel(InspectionViewModel inspectionViewModel)
        {
            inspectionVM = inspectionViewModel;
            _assigments = new ObservableCollection<string>();
            _inspectors = new ObservableCollection<string>();
            _stats = new ObservableCollection<string>();
            _regions = new ObservableCollection<string>();
            SaveCommand = new RelayCommand(saveInspection);

            using (var context = new LocalParkInspectEntities())
            {
                List<Assignment> assignments = context.Assignment.ToList();

                foreach (var assignment in assignments)
                {
                    _assigments.Add(assignment.Description);
                   
                }

                List<Region> regions = context.Region.ToList();

                foreach (var region in regions)
                {
                    _regions.Add(region.Region1);
                }

                List<InspectionStatus> stats = context.InspectionStatus.ToList();

                foreach (var stat in stats)
                {
                    _stats.Add(stat.Description);
                }

                List<Employee> inspectors = context.Employee.ToList();

                foreach (var inspector in inspectors)
                {
                    if(inspector.IsInspecter)
                    {
                        _inspectors.Add(inspector.SurName);
                    }
                    
                }

                StartDatum = DateTime.Now;
                EindDatum = DateTime.Now;



            }

        }
        private ObservableCollection<string> _assigments;
        public ObservableCollection<string> Assignments
        {
            get
            {
                return _assigments;
            }

            set
            {
                _assigments = value;

            }
        }

        private string _selectedAssignment;

        public string SelectedAssignment
        {
            get
            {
                return _selectedAssignment;
            }

            set
            {
                _selectedAssignment = value;
                RaisePropertyChanged("SelectedAssignment");
            }
        }


        private ObservableCollection<string> _regions;

        public ObservableCollection<string> Regions
        {
            get
            {
                return _regions;
            }

            set
            {
                _regions = value;
            }
        }

        private string _selectedRegion;

        public string SelectedRegion
        {
            get
            {
                return _selectedRegion;
            }

            set
            {
                _selectedRegion = value;
                RaisePropertyChanged("SelectedRegion");
            }
        }

        private string _locatie;

        public string Locatie
        {
            get
            {
                return _locatie;
            }

            set
            {
                _locatie = value;
                RaisePropertyChanged("Location");
            }
        }

        private DateTime _startDatum;

        public DateTime StartDatum
        {
            get
            {
                return _startDatum;
            }

            set
            {
                _startDatum = value;
                RaisePropertyChanged("Startdatum");
            }
        }

        private DateTime _eindDatum;

        public DateTime EindDatum
        {
            get
            {
                return _eindDatum;
            }

            set
            {
                _eindDatum = value;
                RaisePropertyChanged("Einddatum");
            }
        }


        private ObservableCollection<string> _stats;

        public ObservableCollection<string> Stats
        {
            get
            {
                return _stats;
            }

            set
            {
                _stats = value;
            }
        }

        private string _selectedStatus;

        public string SelectedStatus
        {
            get
            {
                return _selectedStatus;
            }

            set
            {
                _selectedStatus = value;
                RaisePropertyChanged("SelectedStatus");
            }
        }

        private ObservableCollection<string> _inspectors;

        public ObservableCollection<string> Inspectors
        {
            get
            {
                return _inspectors;
            }

            set
            {
                _inspectors = value;
            }
        }

        private string _selectedInspector;

        public string SelectedInspector
        {
            get
            {
                return _selectedInspector;
            }

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
                List<Assignment> assignments = context.Assignment.ToList();
                List<Region> regions = context.Region.ToList();
                List<InspectionStatus> stats = context.InspectionStatus.ToList();
                List<Employee> inspectors = context.Employee.ToList();

                foreach (var assignment in assignments)
                {
                    if(assignment.Description == SelectedAssignment)
                    {
                        assigmentId = assignment.Id;
                    }

                }

                

                foreach (var region in regions)
                {
                    if(region.Region1 == SelectedRegion)
                    {
                        regionId = region.Id;
                    }
                }

                

                foreach (var stat in stats)
                {
                    if(stat.Description == SelectedStatus)
                    {
                        statusId = stat.Id;
                    }
                }

                

                foreach (var inspector in inspectors)
                {
                    if (inspector.SurName == SelectedInspector)
                    {
                        inspectorId = inspector.Id;
                    }

                }

                if (assigmentId == 0 || statusId == 0 || regionId == 0 || Locatie == null)
                {
                    MessageBox.Show("U heeft iets nog niet ingevuld");
                }

                else
                {
                    Inspection newInspection = new Inspection()
                    {
                        AssignmentId = assigmentId,
                        InspectorId = inspectorId,
                        StatusId = statusId,
                        RegionId = regionId,
                        Location = Locatie,
                        StartDate = StartDatum,
                        EndDate = EindDatum

                    };

                    context.Inspection.Add(newInspection);
                    context.SaveChanges();
                }

               
            }

            inspectionVM.hideAddInspection();
        }


    }
}
