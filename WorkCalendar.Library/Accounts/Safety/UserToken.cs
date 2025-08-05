using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar.Library.Accounts.Safety
{
	public class UserToken
	{
		public string Token { get; set; }
		public DateTime Expires { get; set; }
	}
}
