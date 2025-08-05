namespace WorkCalendar.Client.Data.MessageBox
{
    public interface IMessageBoxHandler
    {
        public string GetMessage();
        public void SetMessage(string message);
        public bool IsVisible();
        public void SetVisible();
        public void SetInvisible();
    }
}
