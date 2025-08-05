
namespace WorkCalendar.Library.Accounts.Safety
{
    public interface IHasher
    {
        string HashText<T>(T toHash);
    }
}
