using GalaSoft.MvvmLight;
using LocalDatabase.Domain;
using ParkInspectGroupC.Encryption;
using ParkInspectGroupC.Miscellaneous;
using ParkInspectGroupC.View;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ParkInspectGroupC.View;

namespace ParkInspectGroupC.ViewModel
{
    public class LoginViewModel : ViewModelBase
	{
        public Employee LoginEmployee { get; set; }
		public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

		private string _loginMessage;
		public string LoginMessage
		{
			get { return _loginMessage; }
			set { _loginMessage = value; RaisePropertyChanged(); }
		}

		private string _username;
		public string Username
		{
			get { return _username; }
			set { _username = value.Trim();  RaisePropertyChanged(); }
		}

		private string _passwordInVM;
		public string PasswordInVM
		{
			get { return _passwordInVM; }
			set { _passwordInVM = value; RaisePropertyChanged(PasswordInVM); }
		}

		public LoginViewModel()
		{
            _loginMessage = string.Empty;
			LoginCommand = new RelayCommand<object>(Login, CanLogin);
            RegisterCommand = new RelayCommand(OpenRegisterWindow);
		}

		private bool CanLogin(object parameter)
		{
			return true;
		}

	    private void OpenRegisterWindow()
	    {
	        Navigator.SetNewView(new EmployeeCreationView());
	    }

		private void Login(object parameter)
		{
			var passwordContainer = parameter as IHavePassword;
			if (passwordContainer != null)
			{
				var secureString = passwordContainer.Password;
				PasswordInVM = PassEncrypt.ConvertToUnsecureString(secureString);

				using(var context = new LocalParkInspectEntities())
				{
					var acc = (from a in context.Account where a.Username.CompareTo(Username) == 0 select a).FirstOrDefault();

					// Account does not excist.
					if (acc == null)
					{
						LoginMessage = "Het ingevoerde gebruikersnaam bestaat niet.";
						return;
					}


					// Check password
					string passToCheck = PasswordInVM + acc.UserGuid;
					if(!ValidateAccount(acc.Password, passToCheck))
					{
						LoginMessage = "De ingevoerde gebruikersnaam en/of wachtwoord is niet geldig.";
						return;
					}

					// Username and Password are correct, continue the application.
                    var emp = (from e in context.Employee where e.Id.CompareTo(acc.EmployeeId) == 0 select e).FirstOrDefault();
                    Properties.Settings.Default.LoggedInEmp = emp;
                   
                    LoginMessage = "Succes!";

                    if (emp.IsManager)
                    {
                        Navigator.SetNewView(new ManagerDashboardView());
                    }
                    else
                    {
                        Navigator.SetNewView(new DashboardView());
                    }
				}

			}
		}

		private bool ValidateAccount(string password, string passwordToCheck)
		{
			string hashedPassword = PassEncrypt.GetPasswordHash(passwordToCheck);

			return (password.CompareTo(hashedPassword) == 0);
		}
	}
}
