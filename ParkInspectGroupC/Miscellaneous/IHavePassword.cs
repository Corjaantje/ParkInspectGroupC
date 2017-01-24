using System.Security;

namespace ParkInspectGroupC.Miscellaneous
{
    internal interface IHavePassword
    {
        SecureString Password { get; }
    }
}