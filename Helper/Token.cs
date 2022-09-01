using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SDM.Helper
{
    public class Token
    {
        private string GenerateToken(string username, string ip, string login)
        {
            string key = "b14ca5898a4e4133bbce2_" + DateTime.Now.ToString("dd/MM/yyyy");
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            string hash = string.Join(":", new string[] { username, ip, login });  //in , out
                            streamWriter.Write(hash);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private static string DecryptToken(string cipherText)
        {
            string key = "b14ca5898a4e4133bbce2_" + DateTime.Today.ToString("dd/MM/yyyy");
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            string text = streamReader.ReadToEnd();
                            string[] parts = text.Split(new char[] { ':' });
                            if (parts.Length == 3)
                            {
                                string username = parts[0];
                                string ip = parts[1];
                                string login = parts[2];

                                return "Done!";
                            }

                            return "Error";
                        }
                    }
                }
            }
        }
    }
}