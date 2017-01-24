using System.Collections.Generic;
using System.Diagnostics;
using GalaSoft.MvvmLight.Command;

namespace ParkInspectGroupC.ViewModel.QuestionnaireModuleViewModels
{
    public class VehicleCountControlVM : QuestionnaireModuleViewModelBase
    {
        public VehicleCountControlVM()
        {
            VehicleTypes = GetKeywordList("Voertuig");
            SubLocations = GetKeywordList("Sublocatie");

            ValueIncrement = new RelayCommand(IncrementValue);
            ValueDecrement = new RelayCommand(DecrementValue);
            KeywordsChanged = new RelayCommand(UpdateCurrentValue);

            CurrentValue = 0;
        }

        public List<string> VehicleTypes { get; set; }
        public List<string> SubLocations { get; set; }

        public string SelectedVehicleType { get; set; }
        public string SelectedSubLocation { get; set; }

        public int CurrentValue { get; set; }

        public RelayCommand ValueIncrement { get; set; }
        public RelayCommand ValueDecrement { get; set; }
        public RelayCommand KeywordsChanged { get; set; }

        private void IncrementValue()
        {
            CurrentValue++;
            UpdateRecords();
        }

        private void DecrementValue()
        {
            CurrentValue--;
            UpdateRecords();
        }

        private void UpdateRecords()
        {
            AddOrUpdateRecord(new string[2] {SelectedVehicleType, SelectedSubLocation}, CurrentValue);
            RaisePropertyChanged("CurrentValue");
        }

        private void UpdateCurrentValue()
        {
            CurrentValue = GetRecordValue(new string[2] {SelectedVehicleType, SelectedSubLocation});
            RaisePropertyChanged("CurrentValue");
        }

        protected override void CleanupForDeletion()
        {
            Debug.WriteLine("hi");
            CurrentValue = 0;
            SelectedVehicleType = VehicleTypes[0];
            SelectedSubLocation = SubLocations[0];
            RaisePropertyChanged(null);
        }
    }
}