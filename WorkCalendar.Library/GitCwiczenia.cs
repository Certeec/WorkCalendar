using System;

namespace WorkCalendar.Library
{
    public class CalendarHelper
    {
        public static int MaxDaysAhead = 30;

        /// <summary>
        /// Checks if given date is a holiday (placeholder).
        /// </summary>
        public static bool IsHoliday(DateTime date)
        {
            // TODO: Integrate with actual holiday calendar
            return false;
        }
    }
}
