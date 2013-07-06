using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.General {
    public class CebraTimeZone {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum UnixTimeType {
        Seconds,
        Milliseconds
    }
    /// <summary>
    /// Different date or time util functions
    /// </summary>
    public static class TimeUtil {
        /// <summary>
        /// Will map an identifier with a timezone
        /// </summary>
        public static Dictionary<int, TimeZoneInfo> timeZones;

        /// <summary>
        /// According to unix time, the starting date is 1970-1-1
        /// </summary>
        private static DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        static TimeUtil() {
            timeZones = new Dictionary<int, TimeZoneInfo>();
            int zoneId = 1;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-12, 0, 0), "UTC-12", "UTC-12"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-11, 0, 0), "UTC-11", "UTC-11"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-10, 0, 0), "UTC-10", "UTC-10"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-9, 0, 0), "UTC-9", "UTC-9"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-8, 0, 0), "UTC-8", "UTC-8"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-7, 0, 0), "UTC-7", "UTC-7"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-6, 0, 0), "UTC-6", "UTC-6"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-5, 0, 0), "UTC-5", "UTC-5"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-4, -30, 0), "UTC-4:30", "UTC-4:30"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-4, 0, 0), "UTC-4", "UTC-4"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-3, -30, 0), "UTC-3:30", "UTC-3:30"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-3, 0, 0), "UTC-3", "UTC-3"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-2, 0, 0), "UTC-2", "UTC-2"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(-1, 0, 0), "UTC-1", "UTC-1"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(0, 0, 0), "UTC 0", "UTC 0"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(1, 0, 0), "UTC+1", "UTC+1"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(2, 0, 0), "UTC+2", "UTC+2"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(3, 0, 0), "UTC+3", "UTC+3"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(3, 30, 0), "UTC+3:30", "UTC+3:30"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(4, 0, 0), "UTC+4", "UTC+4"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(4, 30, 0), "UTC+4:30", "UTC+4:30"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(5, 0, 0), "UTC+5", "UTC+5"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(5, 30, 0), "UTC+5:30", "UTC+5:30"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(5, 45, 0), "UTC+5:45", "UTC+5:45"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(6, 0, 0), "UTC+6", "UTC+6"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(6, 30, 0), "UTC+6:30", "UTC+6:30"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(7, 0, 0), "UTC+7", "UTC+7"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(8, 0, 0), "UTC+8", "UTC+8"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(9, 0, 0), "UTC+9", "UTC+9"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(9, 30, 0), "UTC+9:30", "UTC+9:30"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(10, 0, 0), "UTC+10", "UTC+10"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(11, 0, 0), "UTC+11", "UTC+11"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(12, 0, 0), "UTC+12", "UTC+12"));
            zoneId++;
            timeZones.Add(zoneId, TimeZoneInfo.CreateCustomTimeZone(zoneId.ToString(), new TimeSpan(13, 0, 0), "UTC+13", "UTC+13"));
        }

        /// <summary>
        /// Get all timezones within a IEnumerable
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CebraTimeZone> GetTimeZones() {
            List<CebraTimeZone> result = new List<CebraTimeZone>();
            foreach (var item in timeZones) {
                result.Add(new CebraTimeZone() { Id = item.Key, Name = item.Value.DisplayName });
            }
            return result;
        }

        /// <summary>
        /// Get a mapped time zone based on its id,
        /// if not found it will return UTC time zone info
        /// </summary>
        /// <param name="zoneId">Zone Identifier</param>
        /// <returns></returns>
        public static TimeZoneInfo GetTimeZone(int zoneId) {
            if (timeZones.ContainsKey(zoneId)) {
                return timeZones[zoneId];
            }
            return TimeZoneInfo.Utc;
        }
        /// <summary>
        /// Converts utc time to a specified time zone
        /// </summary>
        /// <param name="utcTime"></param>
        /// <param name="timeZone"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static DateTime UtcToTimeZone(DateTime utcTime, TimeZoneInfo timeZone, bool hour=true) {
            return hour? utcTime.Add(timeZone.BaseUtcOffset) : utcTime.Add(timeZone.BaseUtcOffset).Date;
        }

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