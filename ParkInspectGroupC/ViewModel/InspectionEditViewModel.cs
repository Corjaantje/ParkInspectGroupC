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
   public class InspectionEditViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; set; }
        private InspectionViewModel inspectionVM;

        public InspectionEditViewModel(InspectionViewModel inspectionViewModel)
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

                    if(assignment.Id == inspectionVM.SelectedInspection.AssignmentId)
                    {
                        SelectedAssignment = assignment.Description;
                    }

                }

                List<Region> regions = context.Region.ToList();

                foreach (var region in regions)
                {
                    _regions.Add(region.Region1);

                    if(region.Id == inspectionVM.SelectedInspection.RegionId)
                    {
                        SelectedRegion = region.Region1;
                    }
                }

                List<InspectionStatus> stats = context.InspectionStatus.ToList();

                foreach (var stat in stats)
                {
                    _stats.Add(stat.Description);

                    if(inspectionVM.SelectedInspection.StatusId == stat.Id)
                    {
                        SelectedStatus = stat.Description;
                    }
                    
                }

                List<Employee> inspectors = context.Employee.ToList();

                foreach (var inspector in inspectors)
                {
                    if (inspector.IsInspecter)
                    {
                        _inspectors.Add(inspector.SurName);
                    }

                    if(inspector.Id == inspectionVM.SelectedInspection.InspectorId)
                    {
                        SelectedInspector = inspector.SurName;
                    }

                }

                
                Locatie = inspectionVM.SelectedInspection.Location;
                StartDatum = inspectionVM.SelectedInspection.StartDate;
                EindDatum = inspectionVM.SelectedInspection.EndDate;
                


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
                    if (assignment.Description == SelectedAssignment)
                    {
                        assigmentId = assignment.Id;
                    }

                }



                foreach (var region in regions)
                {
                    if (region.Region1 == SelectedRegion)
                    {
                        regionId = region.Id;
                    }
                }



                foreach (var stat in stats)
                {
                    if (stat.Description == SelectedStatus)
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

                    List<Inspection> inspections = context.Inspection.ToList();

                    foreach(var inspection in inspections)
                    {
                        if(inspection.Id == inspectionVM.SelectedInspection.Id)
                        {
                            inspection.AssignmentId = assigmentId;
                            inspection.InspectorId = inspectorId;
                            inspection.StatusId = statusId;
                            inspection.RegionId = regionId;
                            inspection.Location = Locatie;
                            inspection.StartDate = StartDatum;
                            inspection.EndDate = EindDatum;
                        }
                    }
      
                    context.SaveChanges();
                }


            }

            inspectionVM.hideAddInspection();
        }


    }
}

