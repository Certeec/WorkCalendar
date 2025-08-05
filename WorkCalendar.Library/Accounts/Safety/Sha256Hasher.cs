using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WorkCalendar.Library.Accounts.Safety
{
    public class Sha256Hasher : IHasher
    {
        private const string _salt = "gw2"; //If Changed, you need to migrate all passwords to new Salt
        public string HashText<T>(T toHash)
        {
            //Add Checking if can hash

            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", _salt, toHash);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return  Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }
    }
}
