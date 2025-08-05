using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner
{
	public class SchedulerColorService : ISchedulerColorService
	{
		private IWorkPlannerService _workPlannerService;

		public SchedulerColorService(IWorkPlannerService workPlannerService)
		{
			_workPlannerService = workPlannerService;
		}

		public List<SchedulerDay> GetColors(int userId,DateTime from, DateTime to)
		{
			var tasks = _workPlannerService.GetUserTasksByDate(userId, from, to);

			var daysList = Range(from, to);

			var coloredList = new List<SchedulerDay>();

			foreach(var day in daysList)
			{
				coloredList.Add(new SchedulerDay() { Day = day });
			}

			foreach(var day in coloredList)
			{
				if( tasks.Where(N => N.DateStart == day.Day).FirstOrDefault(n => n.TaskType == "Done") != null)
				{
					day.DayType = "Done";
				}
				else if(tasks.Where(N => N.DateStart == day.Day).FirstOrDefault(n => n.TaskType == "Planned") != null)
				{
					day.DayType = "Planned";
				}
				else if (tasks.Where(N => N.DateStart == day.Day).FirstOrDefault(n => n.TaskType == "Availabe") != null)
				{
					day.DayType = "Availabe";
				}
				else if (tasks.Where(N => N.DateStart == day.Day).FirstOrDefault(n => n.TaskType == "Unavailabe") != null)
				{
					day.DayType = "Unavailabe";
				}

			}

			return coloredList;
		}

		public List<DateTime> Range(DateTime startDate, DateTime endDate)
		{
			return Enumerable.Range(0, (endDate - startDate).Days + 1).Select(d => startDate.AddDays(d)).ToList();
		}
	}
}
