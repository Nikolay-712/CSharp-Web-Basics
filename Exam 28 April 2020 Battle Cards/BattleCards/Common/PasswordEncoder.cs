namespace BattleCards.Common
{
    using System.Security.Cryptography;
    using System.Text;
    using System;

    public class PasswordEncoder
    {
        public static string EncodePassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inarray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inarray);
        }
    }
}
