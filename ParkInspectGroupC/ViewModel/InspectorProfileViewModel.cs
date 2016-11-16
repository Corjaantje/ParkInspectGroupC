using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel
{
   public class InspectorProfileViewModel
    {
       public InspectorProfileViewModel() 
       { 
            //TODO The emp variable should be a parameter to this constructor
           using (var context = new ParkInspectEntities())
           {
               Emp = (from e in context.Employee where e.Id == 1 select e).FirstOrDefault(); 
           }
       }

    }
}
