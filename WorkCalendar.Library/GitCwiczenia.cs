using System;

namespace WorkCalendar.Library
{
    public class CalendarHelper
    {
        public static int MaxDaysAhead = 30;

        /// <summary>
        /// Counts working days (Mon-Fri) between two dates, excluding start date.
        /// </summary>
        public static int GetWorkingDaysCount(DateTime start, DateTime end)
        {
            int count = 0;
            DateTime current = start.AddDays(1);

            while (current <= end)
            {
                if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }
                current = current.AddDays(1);
            }

            return count;
        }

        // WIP: New reporting feature coming
        public static void GenerateReport()
        {
            // TODO: Implement full reporting engine
            throw new NotImplementedException();
        }
    }
}
