using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
				var userToken = await _localStorage.GetItemAsync<UserToken>("UserAuthToken");
                if(!IsTokenValid(userToken))
                    return _anonymous;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", userToken.Token);
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(userToken.Token), "jwtAuthType")));

            }
            catch(Exception E)
            {

            }
            return null;
         
        }

        private bool IsTokenValid(UserToken userToken) => userToken != null && !string.IsNullOrWhiteSpace(userToken.Token) && userToken.Expires > DateTime.Now;

		public void NotifyUserAuthentication(UserToken token)
        {
            if(token.Token != null && token.Expires > DateTime.Now)
            {
                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token.Token), "jwtAuthType"));
                var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
                NotifyAuthenticationStateChanged(authState);
            }
            else
            {
                NotifyUserLogout();
            }
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
