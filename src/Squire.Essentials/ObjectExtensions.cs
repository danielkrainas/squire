namespace Squire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ObjectExtensions
    {
        public static string CoalesceToString(this object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }
    }
}
