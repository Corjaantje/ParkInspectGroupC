using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LocalDatabase.Domain;
using ParkInspectGroupC.Encryption;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class EmployeeCreationViewModel : ViewModelBase
    {
        /*
        private char _numberPrefix;
        public char NumberPrefix
        {
            get { return _numberPrefix; }
            set { _numberPrefix = value; RaisePropertyChanged("NumberPrefix"); }
        }
        */

        private string _city;

        private string _email;

        private string _firstName;

        private string _gender;

        private GenderOption _genderEnum;

        private bool _isInspector;

        private bool _isManager;
        private string _message;

        private string _number;

        private string _password;

        private string _passwordInVM;

        private string _prefix;

        private Employee _selectedManager;


        private Region _selectedRegion;

        private string _street;

        private string _surName;

        private string _telNumber;

        private string _username;

        private string _zipCode;

        public EmployeeCreationViewModel()
        {
            SaveCommand = new RelayCommand<object>(SaveEmployee, CanSaveEmployee);
            ResetFieldsCommand = new RelayCommand(ResetFields);

            // Retreive abailable data from database.
            using (var context = new LocalParkInspectEntities())
            {
                var regionList = context.Region.ToList();
                AvailableRegions = new ObservableCollection<Region>(regionList);

                var managerList = from m in context.Employee where m.IsManager select m;
                AvailableManagers = new ObservableCollection<Employee>(managerList);
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public string PasswordInVM
        {
            get { return _passwordInVM; }
            set
            {
                _passwordInVM = value;
                RaisePropertyChanged(PasswordInVM);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value.Trim();
                RaisePropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value.Trim();
                RaisePropertyChanged("FirstName");
            }
        }

        public string Prefix
        {
            get { return _prefix; }
            set
            {
                _prefix = value.Trim();
                RaisePropertyChanged("Prefix");
            }
        }

        public string SurName
        {
            get { return _surName; }
            set
            {
                _surName = value.Trim();
                RaisePropertyChanged("SurName");
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value.Trim();
                RaisePropertyChanged("Gender");
            }
        }

        public GenderOption GenderEnum
        {
            get { return _genderEnum; }
            set
            {
                _genderEnum = value;
                RaisePropertyChanged("GenderEnum");
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value.Trim();
                RaisePropertyChanged("Street");
            }
        }

        public string Number
        {
            get { return _number; }
            set
            {
                _number = value.Trim();
                RaisePropertyChanged("Number");
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = value.Trim();
                RaisePropertyChanged("City");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value.Trim();
                RaisePropertyChanged("Email");
            }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value.Trim();
                RaisePropertyChanged("ZipCode");
            }
        }

        public string TelNumber
        {
            get { return _telNumber; }
            set
            {
                _telNumber = value.Trim();
                RaisePropertyChanged("TelNumber");
            }
        }

        public Region SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                _selectedRegion = value;
                RaisePropertyChanged("SelectedRegion");
            }
        }

        public Employee SelectedManager
        {
            get { return _selectedManager; }
            set
            {
                _selectedManager = value;
                RaisePropertyChanged("SelectedManager");
            }
        }

        public ObservableCollection<Region> AvailableRegions { get; set; }
        public ObservableCollection<Employee> AvailableManagers { get; set; }

        public bool IsInspector
        {
            get { return _isInspector; }
            set
            {
                _isInspector = value;
                RaisePropertyChanged("IsInspector");
            }
        }

        public bool IsManager
        {
            get { return _isManager; }
            set
            {
                _isManager = value;
                RaisePropertyChanged("IsManager");
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand ResetFieldsCommand { get; set; }


        private void SaveEmployee(object parameter)
        {
            using (var context = new LocalParkInspectEntities())
            {
                var guid = PassEncrypt.GenerateGuid();
                //var tempPass = "parkinspect";
                var passwordContainer = parameter as IHavePassword;
                if (passwordContainer != null)
                {
                    var secureString = passwordContainer.Password;
                    var pass = PassEncrypt.ConvertToUnsecureString(secureString);
                    PasswordInVM = PassEncrypt.GetPasswordHash(pass, guid);

                    var employees = (from a in context.Employee select a).ToList();
                    var newEmployees = employees.Max(u => u.Id);
                    // ## Unreachable if statement, commented out ##
                    //if (newEmployees == null)
                    //{
                    //    newEmployees = 1;
                    //}

                    var nEmployee = new Employee
                    {
                        FirstName = FirstName,
                        SurName = SurName,
                        Gender = GenderEnum.ToString().ElementAt(0).ToString(),
                        City = City,
                        Address = Street + " " + Number,
                        ZipCode = ZipCode,
                        Phonenumber = TelNumber,
                        Email = Email,
                        IsInspecter = true,
                        IsManager = false,
                        Id = (int) newEmployees + 1
                    };
                    if (!string.IsNullOrWhiteSpace(Prefix))
                        nEmployee.Prefix = Prefix;

                    nEmployee.Region =
                        (from r in context.Region where r.Id == SelectedRegion.Id select r).FirstOrDefault();
                    nEmployee.Manager =
                        (from m in context.Employee where m.Id == SelectedManager.Id select m).FirstOrDefault();

                    var accounts = (from a in context.Employee select a).ToList();
                    var newAccount = accounts.Max(u => u.Id);
                    // ## Unreachable if statement, commented out ##
                    //if (newAccount == null)
                    //{
                    //    newEmployees = 1;
                    //}

                    var nAccount = new Account
                    {
                        Username = Username,
                        UserGuid = guid,
                        Password = PasswordInVM,
                        Employee = nEmployee,
                        Id = (int) newAccount + 1
                    };

                    context.Employee.Add(nEmployee);
                    context.Account.Add(nAccount);
                    context.SaveChanges();
                }
            }
            Navigator.SetNewView(new LoginView());
        }

        private bool CanSaveEmployee(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(SurName)
                || string.IsNullOrWhiteSpace(City)
                || string.IsNullOrWhiteSpace(Street)
                || string.IsNullOrWhiteSpace(ZipCode)
                || string.IsNullOrWhiteSpace(TelNumber)
                || string.IsNullOrWhiteSpace(Email))
            {
                Message = "Vul alle velden in.";
                return false;
            }

            if ((Username.Length < 5) || (Username.Length > 25))
            {
                Message = "Username heeft niet de juiste lengte [5,25].";
                return false;
            }

            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                var pass = PassEncrypt.ConvertToUnsecureString(secureString);

                if ((pass.Length < 5) || (pass.Length > 25))
                {
                    Message = "Wachtwoord heeft niet de juiste lengte [5.25].";
                    return false;
                }

                if (!pass.Any(c => char.IsDigit(c)))
                {
                    Message = "Wachtwoord moet minstens een cijfer bevatten [0,9].";
                    return false;
                }

                if (!pass.Any(c => char.IsLetter(c)))
                {
                    Message = "Wachtwoord moet minstens een letter bevatten [A-Z].";
                    return false;
                }
            }

            using (var context = new LocalParkInspectEntities())
            {
                var nameList = (from a in context.Account select a).ToList();

                foreach (var employee in nameList)
                    if (string.Equals(Username, employee.Username, StringComparison.Ordinal))
                    {
                        Message = "Gebruikersnaam bestaad al.";
                        return false;
                    }
            }

            Message = string.Empty;
            return true;
        }

        private void ResetFields()
        {
        }
    }

    public enum GenderOption
    {
        Male,
        Female
    }
}