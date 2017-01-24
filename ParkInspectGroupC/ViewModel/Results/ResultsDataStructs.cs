using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel.Results
{
    public class QuestionnaireResult
    {
        public long InspectionId { get; set; }
        public DateTime DateTime { get; set; }
        public List<string> Keywords { get; set; }
        public int Value { get; set; }
    }

    public class AggregatedResult
    {
        public string Keywords { get; set; }
        public int Value { get; set; }
    }

    public class PlottedResult
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}
