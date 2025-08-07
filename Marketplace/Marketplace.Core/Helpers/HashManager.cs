using System.Security.Cryptography;
using System.Text;

namespace Marketplace.Core.Helpers
{
    public static class HashManager
    {
        private static string GenerateHash(byte[] dataBytes)
        {
            var stringBuilder = new StringBuilder();

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(dataBytes);

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString().Substring(0, 16);
            }
        }

        public static string HashCreate(object data, DateTime salt)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data.ToString() + salt.ToString("yyyyMMddHHmmssffff"));

            return GenerateHash(dataBytes);
        }

        //todo db hashlog when db got created

        private static string RecoverHash(object data, DateTime dateTime)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data.ToString() + dateTime.ToString("yyyyMMddHHmmssffff"));

            return GenerateHash(dataBytes);
        }

        //to check password for sign in or something etc.
        public static bool HashCompare(object data, DateTime oldDateTime, string oldHash)
        {
            //data - password, dateTime - date and time of creating account, oldHasg - saved in db hash of pass or etc.
            var result = RecoverHash(data, oldDateTime);

            return result == oldHash;
        }
    }
}
