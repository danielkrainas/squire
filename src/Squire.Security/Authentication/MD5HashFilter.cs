namespace Squire.Security.Authentication
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class MD5HashFilter : IHashFilter
    {
        public string Calculate(string playerName, string hashTarget)
        {
            var raw = string.Format("@{0}..{1}!", playerName, hashTarget);
            using (var md5 = MD5.Create())
            {
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(raw)));
            }
        }
    }
}
