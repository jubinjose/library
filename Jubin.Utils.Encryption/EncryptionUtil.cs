using System;
using System.Security.Cryptography;
using System.Text;

namespace Jubin.Utils.Encryption
{
    public class EncryptionUtil
    {
        public static string GenerateRandomSaltString()
        {
            return Convert.ToBase64String(GenerateRandomSaltBytes());
        }

        public static string GeneratePBKDF2Hash(string stringToHash, string salt, int numberOfIterations = 10000,
            int desiredHashBytesLength = 20)
        {
            // salt when converetd to bytes should be atleast 8 bytes for Rfc2898DeriveBytes to work
            // salt should be a multiple of 4 for Convert.FromBase64String to work
            // Based on both, salt length >= 12 and should be a multiple of 4
            byte[] saltBytes = Convert.FromBase64String(salt);
            return GeneratePBKDF2Hash(stringToHash, saltBytes, numberOfIterations, desiredHashBytesLength);
        }


        public static byte[] GenerateRandomSaltBytes(int bytesLength = 16)
        {
            byte[] saltBytes = new byte[bytesLength];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return saltBytes;
        }

        public static string GeneratePBKDF2Hash(string stringToHash, byte[] saltBytes, int numberOfIterations = 10000,
            int desiredHashBytesLength = 20)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(stringToHash, saltBytes)
            {
                IterationCount = numberOfIterations
            };
            return Convert.ToBase64String(pbkdf2.GetBytes(desiredHashBytesLength));
        }


        public static string GenerateRandomSaltString(int length, string allowedChars = null)
        {
            //Only use if salt needs to be a specific length. Else use above methods
            // example of allowedChars -> "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            StringBuilder salt = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                while (salt.Length != length)
                {
                    byte[] oneByte = new byte[1];
                    rng.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (allowedChars == null || allowedChars.Contains(character.ToString()))
                    {
                        salt.Append(character);
                    }
                }
            }
            return salt.ToString();
        }

        public static string GenerateHash(string stringToHash, string algorithm = "SHA256")
        {
            //Algorithm parameter value can be any from https://msdn.microsoft.com/en-us/library/wet69s13(v=vs.110).aspx
            if (null == stringToHash)
            {
                stringToHash = String.Empty;
            }

            if (String.IsNullOrEmpty(algorithm))
            {
                throw new ArgumentNullException("A valid algorithm wasn't specified. Specify SHA512, SHA256, SHA1, MD5 etc ");
            }

            using (var alg = HashAlgorithm.Create(algorithm))
            {
                if (null == alg)
                {
                    throw new NotSupportedException("Unsupported algorithm : " + algorithm);
                }
                byte[] hashedPasswordBytes = alg.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }
    }
}
