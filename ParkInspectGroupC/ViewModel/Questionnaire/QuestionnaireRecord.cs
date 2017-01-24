using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel.Questionnaire
{
    public class QuestionnaireRecord
    {
        public QuestionnaireRecord(int ModuleId, string[] Keywords, int value)
        {
            this.ModuleId = ModuleId;
            this.Keywords = Keywords;
            this.value = value;
        }

        //public int QuestionId { get; private set; } // unique within inspection
        public int ModuleId { get; } // must be existing module
        public string[] Keywords { get; }
        public int value { get; set; } // may want to change this to string to be consistent with database and allow more than just numeric input
    }
}
