using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ParkInspectGroupC.ViewModel.Questionnaire
{
    // wil hier eigenlijk geen instantie opslaan; alleen het type om later te instantieren
    public class QuestionnaireControlData
    {
        public QuestionnaireControlData(int Id, string Name, UserControl ModuleUserControl)
        {
            this.Id = Id;
            this.Name = Name;
            this.ModuleUserControl = ModuleUserControl;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public UserControl ModuleUserControl { get; }
    }
}
