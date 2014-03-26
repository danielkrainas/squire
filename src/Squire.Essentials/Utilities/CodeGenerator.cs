namespace Squire.Utilities
{
    using System;
    using System.Text;

    public static class CodeGenerator
    {
        public static string Generate(CodeType type, int length, string custom = null)
        {
            var pool = "";
            switch (type)
            {
                case CodeType.AlphaNumeric: 
                    pool = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;

                case CodeType.Alpha:
                    pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;

                case CodeType.Distinct:
                    pool = "2345679ACDEFHJKLMNPRSTUVWXYZ";
                    break;

                case CodeType.NoZero:
                    pool = "123456789";
                    break;

                case CodeType.Numeric:
                    pool = "0123456789";
                    break;

                case CodeType.Custom:
                    if (!string.IsNullOrWhiteSpace(custom))
                    {
                        throw new ArgumentOutOfRangeException("custom", "value cannot be null, empty, or consist of only whitespace");
                    }

                    pool = custom;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("type");
            }

            var poolChars = pool.ToCharArray();
            var poolSize = poolChars.Length;
            StringBuilder sb = new StringBuilder();
            var r = new Random();
            for (var i = 0; i < length; i++)
            {
                sb.Append(poolChars[r.Next(0, poolSize - 1)]);
            }

            return sb.ToString();
        }
    }
}
