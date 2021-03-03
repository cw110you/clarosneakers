using System;
using System.Security.Cryptography;

namespace webstore.Helpers
{
    public static class HashHelper
    {
        public static string Hash(string password, string salt)
        {
            byte[] passwordAndSalt = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hash = new SHA256Managed().ComputeHash(passwordAndSalt);

            return Convert.ToBase64String(hash);
        }
        public static PasswordWithSalt Hash(string password)
        {
            string salt = Guid.NewGuid().ToString();
            byte[] passwordAndSalt = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hash = new SHA256Managed().ComputeHash(passwordAndSalt);

            PasswordWithSalt pws = new PasswordWithSalt()
            {
                salt = salt,
                password = Convert.ToBase64String(hash)
            };

            return pws;
        }
    }

    public class PasswordWithSalt
    {
        public string password { get; set; }
        public string salt { get; set; }
    }
}
