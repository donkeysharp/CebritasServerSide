using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.General {
    public enum UnixTimeType {
        Seconds,
        Milliseconds
    }
    /// <summary>
    /// Different date or time util functions
    /// </summary>
    public static class TimeUtil {
        /// <summary>
        /// According to unix time, the starting date is 1970-1-1
        /// </summary>
        private static DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a datetime object to unix format depending in
        /// the chosen type (Milliseconds|Seconds)
        /// </summary>
        /// <param name="dateTime">DateTime object to be converted</param>
        /// <param name="type">Type of the value to be retrieved</param>
        /// <returns></returns>
        public static long DateTimeToUnixTime(DateTime dateTime, UnixTimeType type) {
            TimeSpan timeSpan = dateTime - EPOCH;

            if (type == UnixTimeType.Milliseconds) {
                return (long)timeSpan.TotalMilliseconds;
            } else {
                return (long)timeSpan.TotalSeconds;
            }
        }

        /// <summary>
        /// Converts a unixtime value (milliseconds or seconds according of type)
        /// into a DateTime object representing the value.
        /// </summary>
        /// <param name="unixTime">UnixTime value</param>
        /// <param name="type">Type of the unixtime value</param>
        /// <returns></returns>
        public static DateTime UnixTimeToDateTime(long unixTime, UnixTimeType type) {
            if (type == UnixTimeType.Milliseconds) {
                return EPOCH.AddMilliseconds(unixTime);
            } else {
                return EPOCH.AddSeconds(unixTime);
            }
        }
    }
}