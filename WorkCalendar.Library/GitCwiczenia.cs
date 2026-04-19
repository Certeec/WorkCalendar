using System;

namespace WorkCalendar.Library
{
    public class CalendarHelper
    {
        public static int MaxDaysAhead = 30;

        /// <summary>
        /// Counts working days (Mon-Fri) between two dates, excluding start date.
        /// </summary>
        public static int GetWorkingDaysCount(DateTime startDate, DateTime endDate)
        {
            int workingDaysCount = 0;
            DateTime currentDate = startDate.AddDays(1);

            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDaysCount++;
                }
                currentDate = currentDate.AddDays(1);
            }

            return workingDaysCount;
        }

        // WIP: New reporting feature coming
        public static void GenerateReport()
        {
            // TODO: Implement full reporting engine
            throw new NotImplementedException();
        }
    }
}
