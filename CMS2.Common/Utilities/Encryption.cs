using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Common.Utilities
{
    public static class Encryption
    {
        public static byte[] EncryptPassword(string cleartext)
        {
            byte[] encryptedPassword = new byte[] { };

            try
            {
                byte[] password = Encoding.ASCII.GetBytes("@Pc@R60");
                byte[] salt = Encoding.Default.GetBytes("M1cr0b!zOnE3ncR%p+!oNK3y");

                PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, salt);

                using (Rijndael Rijndael = Rijndael.Create())
                {

                    byte[] key = passwordDeriveBytes.GetBytes(256 / 8);
                    byte[] IV = passwordDeriveBytes.GetBytes(16);

                    // Encrypt the string to an array of bytes.                   
                    encryptedPassword = EncryptStringToBytes(cleartext, key, IV);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return encryptedPassword;
        }

        public static string DecryptPassword(byte[] ciphertext)
        {
            string decryptedPassword = "";
            byte[] password = Encoding.ASCII.GetBytes("@Pc@R60");
            byte[] salt = Encoding.Default.GetBytes("M1cr0b!zOnE3ncR%p+!oNK3y");

            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, salt);

            try
            {
                // Create a new instance of the Rijndael
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (Rijndael myRijndael = Rijndael.Create())
                {
                    byte[] key = passwordDeriveBytes.GetBytes(256 / 8);
                    byte[] IV = passwordDeriveBytes.GetBytes(16);

                    
                    // Decrypt the bytes to a string.
                    decryptedPassword = DecryptStringFromBytes(ciphertext, key, IV);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return decryptedPassword;
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}
