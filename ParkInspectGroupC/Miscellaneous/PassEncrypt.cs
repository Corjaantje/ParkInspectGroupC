using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace ParkInspectGroupC.Encryption
{
    public class PassEncrypt
    {
        // <summary>
        // GetPasswordHash
        // </summary>
        // <returns>
        // Return the hash value from the password and identical guid. 
        // </returns>
        // <param name="passString">
        // passString; the original password from a textbox 
        // </param>
        // <param name="guid">
        // guid; a identical string generate form each account. Retreived from the database.
        // </param>
        public static string GetPasswordHash(string passString, string guid)
        {
            string hashedPassword;
            var stringToHash = passString += guid;

            hashedPassword = GenerateHashValue(stringToHash);

            // Dit is even om Corné zijn Github skills te testen.

            return hashedPassword;
        }

        public static string GetPasswordHash(string passString)
        {
            string hashedPassword;

            hashedPassword = GenerateHashValue(passString);

            return hashedPassword;
        }

        // <summary>
        // Generates a random guid string.
        // </summary>
        public static string GenerateGuid()
        {
            var userGuid = Guid.NewGuid();
            return userGuid.ToString();
        }

        private static string GenerateHashValue(string value)
        {
            var sha1 = SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }

        // <summary>
        // Retreives a secure password.
        // </summary>
        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}