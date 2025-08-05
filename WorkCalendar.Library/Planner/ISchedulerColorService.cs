using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner
{
	public interface ISchedulerColorService
	{
		List<SchedulerDay> GetColors(int userId, DateTime from, DateTime to);
	}
}
