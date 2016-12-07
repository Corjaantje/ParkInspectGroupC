using System;
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
		public EmployeeCreationViewModel()
		{
			SaveCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
			ResetFieldsCommand = new RelayCommand(ResetFields);

			// Retreive abailable data from database.
			using (var context = new ParkInspectEntities())
			{
				var regionList = context.Region.ToList();
				AvailableRegions = new ObservableCollection<Region>(regionList);

				var managerList = (from m in context.Employee where m.IsManager == true select m);
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
					SurName = this.SurName,
					Gender = GenderEnum.ToString().ElementAt(0).ToString(),
					City = this.City,
					Address = this.Street + " " + this.Number,
					ZipCode = this.ZipCode,
					Phonenumber = this.TelNumber,
					Email = this.Email,
					IsInspecter = this.IsInspector,
					IsManager = this.IsManager,
				};
				if (!string.IsNullOrWhiteSpace(Prefix))
					nEmployee.Prefix = this.Prefix;

				nEmployee.Region = (from r in context.Region where r.Id == SelectedRegion.Id select r).FirstOrDefault();
				nEmployee.Manager = (from m in context.Employee where m.Id == SelectedManager.Id select m).FirstOrDefault();
                
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
			if (string.IsNullOrWhiteSpace(Username)
			    || string.IsNullOrWhiteSpace(SurName)
			    || string.IsNullOrWhiteSpace(City)
			    || string.IsNullOrWhiteSpace(Street)
			    || string.IsNullOrWhiteSpace(ZipCode)
			    || string.IsNullOrWhiteSpace(TelNumber)
			    || string.IsNullOrWhiteSpace(Email))
				return false;

			return true;
		}

		private void ResetFields()
		{
			
		}

		private string _username;
		public string Username
		{
			get { return _username; }
			set { _username = value.Trim(); RaisePropertyChanged("Username"); }
		}

		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value.Trim(); RaisePropertyChanged("FirstName"); }
		}

		private string _prefix;
		public string Prefix
		{
			get { return _prefix; }
			set { _prefix = value.Trim(); RaisePropertyChanged("Prefix"); }
		}

		private string _surName;
		public string SurName
		{
			get { return _surName; }
			set { _surName = value.Trim(); RaisePropertyChanged("SurName"); }
		}

		private string _gender;
		public string Gender
		{
			get { return _gender; }
			set { _gender = value.Trim(); RaisePropertyChanged("Gender"); }
		}

		private GenderOption _genderEnum;
		public GenderOption GenderEnum
		{
			get { return _genderEnum; }
			set { _genderEnum = value; RaisePropertyChanged("GenderEnum"); }
		}

		private string _street;
		public string Street
		{
			get { return _street; }
			set { _street = value.Trim(); RaisePropertyChanged("Street"); }
		}

		private string _number;
		public string Number
		{
			get { return _number; }
			set { _number = value.Trim(); RaisePropertyChanged("Number"); }
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
			set { _city = value.Trim(); RaisePropertyChanged("City"); }
		}

		private string _email;
		public string Email
		{
			get { return _email; }
			set { _email = value.Trim(); RaisePropertyChanged("Email"); }
		}

		private string _zipCode;
		public string ZipCode
		{
			get { return _zipCode; }
			set { _zipCode = value.Trim(); RaisePropertyChanged("ZipCode"); }
		}

		private string _telNumber;
		public string TelNumber
		{
			get { return _telNumber; }
			set { _telNumber = value.Trim(); RaisePropertyChanged("TelNumber"); }
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

	}

	public enum GenderOption
	{
		Male,
		Female
	}
}
