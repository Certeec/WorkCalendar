using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar.Library.Utils
{
	public static class DateTimeExtensions
	{
		public static DateTime SetEndOfDay(this DateTime date) => date.AddHours(23).AddMinutes(23).AddSeconds(59);

	}
}
