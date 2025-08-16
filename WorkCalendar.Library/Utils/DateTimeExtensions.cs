

namespace WorkCalendar.Library.Utils
{
	public static class DateTimeExtensions
	{
        public static DateTime SetEndOfDay(this DateTime date) => date.Date.AddDays(1).AddTicks(-1);
        public static DateTime SetFirstDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);
        public static DateTime SetLastDayOfMonth(this DateTime date) =>
        new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
    }
}
