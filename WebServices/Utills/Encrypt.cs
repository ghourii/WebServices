using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebServices.Utills
{
    public static class Encrypt
    {
        public static string Encode(string phrase)
        {
            try
            {
                byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(phrase);
                string s = Convert.ToBase64String(encbuff);
                char[] ar = s.ToCharArray();
                Array.Reverse(ar);
                string er = new string(ar);
              
                return er;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string Reverse(string phrase)
        {
            try
            {
                char[] ar = phrase.ToCharArray();
                Array.Reverse(ar);
                string er = new string(ar);
                return er;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string Decode(string phrase)
        {
            try
            {
                char[] ar = phrase.ToCharArray();
                Array.Reverse(ar);
                phrase = new string(ar);              
                byte[] decbuff = Convert.FromBase64String(phrase);
                return System.Text.Encoding.UTF8.GetString(decbuff);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Encryption(string input, string key)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(input);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    input = Convert.ToBase64String(ms.ToArray());
                }
            }
            input = input.Replace("/", "--");
            input = input.Replace("+", "___");
            return input;
        }
        public static string Decryption(string input, string key)
        {
            input = input.Replace("___", "+");
            input = input.Replace(" ", "+");
            input = input.Replace("--", "/");
            byte[] cipherBytes = Convert.FromBase64String(input);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    input = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return input;
        }

    }
}