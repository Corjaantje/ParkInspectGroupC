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

       private string _adress;
       public string Adress
       {
           get { return _adress; }
           set { _adress = value; RaisePropertyChanged("Adress"); }
       }
       private string _email;
       public string Email
       {
           get { return _email; }
           set { _email = value; RaisePropertyChanged("Email"); }
       }

       private string _phoneNumber;
       public string PhoneNumber
       {
           get { return _phoneNumber; }
           set { _phoneNumber = value; RaisePropertyChanged("PhoneNumber"); }
       }

       private string _status;
       public string Status
       {
           get { return _status; }
           set { _status = value; RaisePropertyChanged("Status"); }
       }
       public InspectorProfileViewModel() 
       {
            //TODO The emp variable should be a parameter to this constructor
           using (var context = new ParkInspectEntities())
           {
               Emp = (from e in context.Employee where e.Id == 1 select e).FirstOrDefault();
           }
           Name = Emp.FirstName + " " + Emp.Prefix + " " + Emp.SurName + "(" + Emp.Gender + ")";
           Adress = Emp.Address + ", " + Emp.ZipCode + ", " + Emp.City;
           Email = Emp.Email;
           PhoneNumber = Emp.Phonenumber;
       }

    }
}
