using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDatabase.Domain
{
    public class SaveDeleteMessage
    {
        public string Action { get; set; }
        public string Message { get; set; }
    }
}