using GalaSoft.MvvmLight;
using ParkInspectGroupC.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkInspectGroupC.ViewModel
{
    public class InspectorProfileViewModel : ViewModelBase
    {
       Employee Emp;
       private string _name;
       public string Name
       {
           get { return _name; }
           set { _name = value; RaisePropertyChanged("Name"); }
       }
       public InspectorProfileViewModel() 
       {
           Name = "Roy Tersluijsen";
            //TODO The emp variable should be a parameter to this constructor
           using (var context = new ParkInspectEntities())
           {
               Emp = (from e in context.Employee where e.Id == 1 select e).FirstOrDefault();
           }
           Name = Emp.FirstName + " " + Emp.Prefix + " " + Emp.SurName;
           Console.WriteLine("Name: " + Name);
       }

    }
}
