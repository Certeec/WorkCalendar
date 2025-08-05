using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar.Library.Accounts.Safety
{
    public interface IHasher
    {
        string HashText<T>(T toHash);
    }
}
