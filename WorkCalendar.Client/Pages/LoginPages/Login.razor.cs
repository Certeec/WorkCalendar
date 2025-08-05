namespace WorkCalendar.Client.Pages.LoginPages
{
    public partial class Login
    {
        private string login;
        private string password;

        private async Task LoginIn()
        {
            if (login == null || login == "")
            {
                messageHandler.SetMessage("Login is empty");
                return;
            }

            if (password == null || password == "")
            {
                messageHandler.SetMessage("Password is empty");
                return;
            }

            var result = await userActions.LoginIn(login, password);

            if (result.Token == null)
            {
                messageHandler.SetMessage("Your login or password is wrong");
                return;

            }
            else
            {
                messageHandler.SetInvisible();
                navigationManager.NavigateTo("/UserAccountSettings");
            }
        }
    }
}