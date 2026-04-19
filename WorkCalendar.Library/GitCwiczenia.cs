using System;

namespace WorkCalendar.Library
{
    public class CalendarHelper
    {
        public static int MaxDaysAhead = 30;

        /// <summary>
        /// Formats a date range as "dd.MM.yyyy - dd.MM.yyyy".
        /// </summary>
        public static string FormatDateRange(DateTime start, DateTime end)
        {
            return $"{start:dd.MM.yyyy} - {end:dd.MM.yyyy}";
            //test zmiana
        }
    }
}
