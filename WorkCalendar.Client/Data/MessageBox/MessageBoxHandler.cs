namespace WorkCalendar.Client.Data.MessageBox
{
    public class MessageBoxHandler : IMessageBoxHandler
    {
        private string _value = string.Empty;
        private bool _visible = false;
        public string GetMessage()
        {
            return _value;
        }

        public void SetMessage(string message)
        {
            _value = message;
            _visible = true;
        }

        public bool IsVisible()
        {
            return _visible;
        }

        public void SetVisible()
        {
            _visible = true;
        }

        public void SetInvisible()
        {
            _visible = false;
        }
    }
}
