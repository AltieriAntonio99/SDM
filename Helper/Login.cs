using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using SDM.Models.Database;

namespace SDM.Helper
{
    public class Authentication
    {
        public bool Login(string username, string password, string role, bool checkRole = false)
        {
            using (var context = new SDMEntities())
            {
                string passwordCrypt = ComputeHash(password, new SHA256CryptoServiceProvider(), "CrYpTPaSsWoRd789458727251251SdmSold???!é=r");
                var result = context.Users.FirstOrDefault(i => i.Username == username && i.Password == passwordCrypt);

                if (checkRole)
                {
                    return result != null && role != "demo" && role != "smartJob";
                }
                else
                {
                    return result != null;
                }
                
            }
        }

        public Users GetLoginUser(string username, string password)
        {
            using (var context = new SDMEntities())
            {
                string passwordCrypt = ComputeHash(password, new SHA256CryptoServiceProvider(), "CrYpTPaSsWoRd789458727251251SdmSold???!é=r");
                var result = context.Users.FirstOrDefault(i => i.Username == username & i.Password == passwordCrypt);
                if(result != null)
                {
                    result.Sedi = context.Users.FirstOrDefault(i => i.Username == username & i.Password == passwordCrypt).Sedi;
                    result.Roles = context.Users.FirstOrDefault(i => i.Username == username & i.Password == passwordCrypt).Roles;
                }
                

                return result;
            }
        }

        private static string ComputeHash(string input, HashAlgorithm algorithm, string saltString)
        {
            Byte[] salt = Encoding.UTF8.GetBytes(saltString);
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Combine salt and input bytes
            Byte[] saltedInput = new Byte[salt.Length + inputBytes.Length];
            salt.CopyTo(saltedInput, 0);
            inputBytes.CopyTo(saltedInput, salt.Length);

            Byte[] hashedBytes = algorithm.ComputeHash(saltedInput);

            return Convert.ToBase64String(hashedBytes);
        }

        public bool ResetPassword(string username, string oldPassword, string newPassword)
        {
            using (var context = new SDMEntities())
            {
                string oldPasswordCrypt = ComputeHash(oldPassword, new SHA256CryptoServiceProvider(), "CrYpTPaSsWoRd789458727251251SdmSold???!é=r");
                string newPasswordCrypt = ComputeHash(newPassword, new SHA256CryptoServiceProvider(), "CrYpTPaSsWoRd789458727251251SdmSold???!é=r");
                var result = context.Users.FirstOrDefault(i => i.Username == username & i.Password == oldPasswordCrypt);

                if(result != null)
                {
                    result.Password = newPasswordCrypt;

                    return context.SaveChanges() > 0;
                }
            }

            return false;
        }
    }
}