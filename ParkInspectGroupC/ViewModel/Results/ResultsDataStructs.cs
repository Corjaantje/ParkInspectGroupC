using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel.Results
{
    public struct QuestionnaireResult
    {
        public long InspectionId;
        public DateTime DateTime;
        public string[] Keywords;
        public int Value;
    }

    public struct AggregatedResult
    {
        public string Keywords;
        public int Value;
    }
}
