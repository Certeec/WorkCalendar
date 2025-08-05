namespace WorkCalendar.Client.Data.Scheduler
{
    public class SchedulerInfoData
    {
        private int _rounding = 2;
        private double _totalHours;
        private double _expectedMoney;
        private double _moneyEarned;
        private double _bonus;
        private double _hours;
		private double _totalPay;

		public double TotalHours
        {
            get
            {
                return _totalHours;
            }
            set
            {
                _totalHours = Math.Round(value, _rounding);
            }
        }

        public double ExpectedMoney
        {
            get
            {
                return _expectedMoney;
            }
            set
            {
                _expectedMoney = Math.Round(value, _rounding);
            }
        }

        public double MoneyEarned
        {
            get
            {
                return _moneyEarned;
            }
            set
            {
                _moneyEarned = Math.Round(value, _rounding);
            }
        }

        public double bonus
        {
            get
            {
                return _bonus;
            }
            set
            {
                _bonus = Math.Round(value, _rounding);
            }
        }

        public double Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                _hours = Math.Round(value, _rounding);
            }
        }
		public double TotalPay
		{
			get
			{
				return _totalPay;
			}
			set
			{
				_totalPay = Math.Round(value, _rounding);
			}
		}
	}
}
