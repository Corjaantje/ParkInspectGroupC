using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParkInspectGroupC.View.QuestionnaireModules
{
    /// <summary>
    /// Interaction logic for VehicleCountControl.xaml
    /// </summary>
    public partial class VehicleCountControl : UserControl, INotifyPropertyChanged // The UserControl modules aim to be self-contained, hence changes directly in the code-behind
    {
        public List<string> VehicleTypes { get; set; }
        public List<string> SubLocations { get; set; }

        public string SelectedVehicleType { get; set; }
        public string SelectedSubLocation { get; set; }

        public int CurrentValue { get; set; }

        public ICommand ValueIncrement { get; set; }
        public ICommand ValueDecrement { get; set; }

        public VehicleCountControl()
        {
            InitializeComponent();
            DataContext = this;

            VehicleTypes = new List<string>();
            VehicleTypes.Add("Personenauto");
            VehicleTypes.Add("Vrachtwagen");

            SubLocations = new List<string>();
            SubLocations.Add("Noord");
            SubLocations.Add("Zuid");

            CurrentValue = 0;
        }

        

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
            RaisePropertyChanged("CurrentValue");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void textBox_AmountInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
