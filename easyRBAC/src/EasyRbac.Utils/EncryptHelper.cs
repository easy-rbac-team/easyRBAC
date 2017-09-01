using System;
using System.Security.Cryptography;
using System.Text;

namespace EasyRbac.Utils
{
    public class EncryptHelper
    {
        public string GenerateSalt()
        {
            using(var rngCsp = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[256];
                rngCsp.GetBytes(bytes);
                return this.ToHex(bytes);
            }            
        }

        public string Sha256Encrypt(string input)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = mySHA256.ComputeHash(bytes);
            return this.ToHex(hash);
        }

        public string ToHex(byte[] input)
        {
            var sb = new StringBuilder();
            int i;
            for (i = 0; i < input.Length; i++)
            {
                sb.AppendFormat("{0:X2}", input[i]);
                if ((i % 4) == 3) Console.Write(" ");
            }
            return sb.ToString();
        }

    }
}
