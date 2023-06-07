using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Financical_Traker.Core.AccountManagement
{
	public class User
	{
        public string name;
        public string password;
        public string mail;
        public string defaultCurrency;
        public string UTC;
        public string Hash;

        public User(string newName, string newPassword, string newMail, string newDedaultCurrency, string newUTC)
        {
            name = newName;
            password = newPassword;
            mail = newMail;
            defaultCurrency = newDedaultCurrency;
            UTC = newUTC;

            byte[] inputBytes = Encoding.UTF8.GetBytes(mail);
            byte[] hashBytes = SHA256.HashData(inputBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            Hash = hash;
        }

        [JsonConstructor]
        public User(string newName, string newPassword, string newMail, string newDedaultCurrency, string newUTC, string hash)
        {
            name = newName;
            password = newPassword;
            mail = newMail;
            defaultCurrency = newDedaultCurrency;
            UTC = newUTC;
            Hash = hash;
        }

        public User(string newMail, string newPassword)
        {
            mail = newMail;
            password = newPassword;

            byte[] inputBytes = Encoding.UTF8.GetBytes(mail);
            byte[] hashBytes = SHA256.HashData(inputBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            Hash = hash;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                byte[] inputBytes = Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = SHA256.HashData(inputBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                Hash = hash;
            }
        }

        public string DefaultCurrency
        {
            get { return defaultCurrency; }
            set { defaultCurrency = value; }
        }

        public string UTC_value
        {
            get { return UTC; }
            set { UTC = value; }
        }
    }
}
