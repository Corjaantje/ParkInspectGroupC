using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ParkInspectGroupC.DOMAIN;
using ParkInspectGroupC.Encryption;

namespace ParkInspectGroupC.ViewModel
{
	public class EmployeeCreationViewModel : ViewModelBase
	{
		private string _username;
		public string Username
		{
			get { return _username; }
			set { _username = value; RaisePropertyChanged("Username"); }
		}

		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; RaisePropertyChanged("FirstName"); }
		}

		private string _prefix;
		public string Prefix
		{
			get { return _prefix; }
			set { _prefix = value; RaisePropertyChanged("Prefix"); }
		}

		private string _surName;
		public string SurName
		{
			get { return _surName; }
			set { _surName = value; RaisePropertyChanged("SurName"); }
		}

		private string _gender;
		public string Gender
		{
			get { return _gender; }
			set { _gender = value; RaisePropertyChanged("Gender"); }
		}

		private string _street;
		public string Street
		{
			get { return _street; }
			set { _street = value; RaisePropertyChanged("Street"); }
		}

		private string _number;
		public string Number
		{
			get { return _number; }
			set { _number = value; RaisePropertyChanged("Number"); }
		}

		/*
		private char _numberPrefix;
		public char NumberPrefix
		{
			get { return _numberPrefix; }
			set { _numberPrefix = value; RaisePropertyChanged("NumberPrefix"); }
		}
		*/

		private string _city;
		public string City
		{
			get { return _city; }
			set { _city = value; RaisePropertyChanged("City"); }
		}

		private string _email;
		public string Email
		{
			get { return _email; }
			set { _email = value; RaisePropertyChanged("Email");} 
		}

		private string _zipCode;
		public string ZipCode
		{
			get { return _zipCode; }
			set { _zipCode = value; RaisePropertyChanged("ZipCode"); }
		}

		private string _telNumber;
		public string TelNumber
		{
			get { return _telNumber; }
			set { _telNumber = value; RaisePropertyChanged("TelNumber"); }
		}

		private Region _selectedRegion;
		public Region SelectedRegion
		{
			get { return _selectedRegion; }
			set { _selectedRegion = value; RaisePropertyChanged("SelectedRegion"); }
		}

		private Employee _selectedManager;
		public Employee SelectedManager
		{
			get { return _selectedManager; }
			set { _selectedManager = value; RaisePropertyChanged("SelectedManager"); }
		}
		
		public ObservableCollection<Region> AvailableRegions { get; set; }
		public ObservableCollection<Employee> AvailableManagers { get; set; }

		private bool _isInspector;
		public bool IsInspector
		{
			get { return _isInspector; }
			set { _isInspector = value; RaisePropertyChanged("IsInspector"); }
		}

		private bool _isManager;
		public bool IsManager
		{
			get { return _isManager; }
			set { _isManager = value; RaisePropertyChanged("IsManager"); }
		}

		public ICommand SaveCommand { get; set; }
		public ICommand ResetFieldsCommand { get; set; }

		public EmployeeCreationViewModel()
		{
			SaveCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
			ResetFieldsCommand = new RelayCommand(ResetFields);

			// Retreive abailable data from database.
			using (var context = new ParkInspectEntities())
			{
				var regionList = context.Region.ToList();
				AvailableRegions = new ObservableCollection<Region>(regionList);

				var managerList = (from m in context.Employee where m.Manager == true select m);
				AvailableManagers = new ObservableCollection<Employee>(managerList);
			}
		}

		private void SaveEmployee()
		{
			using (var context = new ParkInspectEntities())
			{
				var guid = PassEncrypt.GenerateGuid();
				var tempPass = "parkinspect";
				var nEmployee = new Employee
				{
					FirstName = this.FirstName,
					Prefix = this.Prefix,
					SurName = this.SurName,
					Gender = "M",
					City = this.City,
					Address = this.Street + " " + this.Number,
					ZipCode = this.ZipCode,
					Phonenumber = this.TelNumber,
					Email = this.Email,
					Region = this.SelectedRegion,
					Inspecter = this.IsInspector,
					Manager = this.IsManager,
				};
				var nAccount = new Account
				{
					Username = this.Username,
					UserGuid = guid,
					Password = PassEncrypt.GetPasswordHash(tempPass, guid),
					Employee = nEmployee
				};

				context.Employee.Add(nEmployee);
				context.Account.Add(nAccount);
				context.SaveChanges();
			}
		}

		private bool CanSaveEmployee()
		{
			return true;
		}

		private void ResetFields()
		{
			
		}

		public enum GenderOption
		{
			Male,
			Female
		}
	}
}
