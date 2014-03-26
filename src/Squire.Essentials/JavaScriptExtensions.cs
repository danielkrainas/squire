namespace Squire
{
    using System;

    public static class JavaScriptExtensions
    {
        public static double ToJavaScriptTimeStampUtc(this DateTime value)
        {
            return value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        public static DateTime FromJavaScriptTimeStampUtc(this double value)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(value);
        }
    }
}
