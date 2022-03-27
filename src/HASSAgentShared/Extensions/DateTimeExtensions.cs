using System;
using System.Collections.Generic;
using System.Text;

namespace HASSAgent.Shared.Extensions
{
    /// <summary>
    /// Extension for the DateTime object
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts the DateTime object to a timezone-containing string
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToTimeZoneString(this DateTime datetime) => $"{datetime.ToUniversalTime():u}";
    }
}
