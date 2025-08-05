using Heron.MudCalendar;

namespace WorkCalendar.Client.Data.Scheduler
{
	public class SchedulerTask : CalendarItem
	{
		private DateTime _dateStart;
		private DateTime _dateEnd;
		private string _place;
		private double _revenue;
		private double _revenueParticipation;
		private bool _bonusByRevenue;

		public int TaskId { get; set; }
		public bool ShowDetails { get; set; }
		public DateTime DateStart { get { return _dateStart; }
			set { _dateStart = value;
                Start = value;
			}
		}
		public DateTime DateEnd { get { return _dateEnd; }
			set { _dateEnd = value;
                End = value;
			}
		}
		public double TimeLength { 
			get 
			{
				TimeSpan hours = DateEnd - DateStart;
				return hours.TotalHours;
			}
		}
		public string Place { get { return _place; } set
			{
				_place = value;
                Text = Place;
			}
		}
		public string TaskType { get; set; }
		public string Description { get; set; }
		public double MoneyPerHour { get; set; }
		public double Bonus { get; set; }
		public bool IsActive { get; set; }
		public double Revenue { get { return _revenue; } 
			set {
				_revenue = value;
				if(BonusByRevenue)
                    Bonus = _revenue * (0.01* RevenueParticipation); 
			}
		}
        public double RevenueParticipation { get { return _revenueParticipation; } set
			{ _revenueParticipation = value;
                if (BonusByRevenue)
                    Bonus = Revenue * (0.01 *_revenueParticipation);
			} }
		public bool BonusByRevenue { get { return _bonusByRevenue; } set 
			{ _bonusByRevenue = value;
				if(value)
                    Bonus = Revenue * (0.01 * _revenueParticipation);
            } }

		public double PremiumPoints { get; set; }
    }
}
