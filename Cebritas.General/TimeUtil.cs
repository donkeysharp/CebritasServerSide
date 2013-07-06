using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cebritas.General {
    /// <summary>
    /// Constants for all timezones, it should be an enum, but I'm lazy to
    /// change it, and time is running :S
    /// </summary>
    public class TimeZones {
        public const byte UTC_12 = 1;
        public const byte UTC_11 = 2;
        public const byte UTC_10 = 3;
        public const byte UTC_9 = 4;
        public const byte UTC_8 = 5;
        public const byte UTC_7 = 6;
        public const byte UTC_6 = 7;
        public const byte UTC_5 = 8;
        public const byte UTC_430 = 9;
        public const byte UTC_4 = 10;
        public const byte UTC_330 = 11;
        public const byte UTC_3 = 12;
        public const byte UTC_2 = 13;
        public const byte UTC_1 = 14;
        public const byte UTC_0 = 15;
        public const byte UTC1 = 16;
        public const byte UTC2 = 17;
        public const byte UTC3 = 18;
        public const byte UTC330 = 19;
        public const byte UTC4 = 20;
        public const byte UTC430 = 21;
        public const byte UTC5 = 22;
        public const byte UTC530 = 23;
        public const byte UTC545 = 24;
        public const byte UTC6 = 25;
        public const byte UTC630 = 26;
        public const byte UTC7 = 27;
        public const byte UTC8 = 28;
        public const byte UTC9 = 29;
        public const byte UTC930 = 30;
        public const byte UTC10 = 31;
        public const byte UTC11 = 32;
        public const byte UTC12 = 33;
        public const byte UTC13 = 34;
    }
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

        /// <summary>
        /// Converts a datetime that is in a given time zone to UTC timezone
        /// </summary>
        /// <param name="dateTime">Date to be converted</param>
        /// <param name="timeZone">Date's timezone</param>
        /// <returns></returns>
        public static DateTime ConvertDateTimeToUtc(DateTime dateTime, TimeZoneInfo timeZone) {
            DateTime input = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Unspecified);
            TimeZoneInfo utcTimeZone = GetTimeZone(TimeZones.UTC_0);
            DateTime temp = TimeZoneInfo.ConvertTime(input, timeZone, utcTimeZone);
            DateTime result = new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, temp.Second, DateTimeKind.Utc);
            return result;
        }
    }
}