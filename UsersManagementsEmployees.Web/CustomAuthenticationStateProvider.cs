using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace UsersManagementsEmployees.Web
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly TokenServiceReader _tokenServiceReader;
        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, TokenServiceReader tokenServiceReader)
        {
            _jsRuntime = jsRuntime;
            _tokenServiceReader = tokenServiceReader;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var username = await _tokenServiceReader.GetUsernameFromTokenAsync();
            var identity = username != null
                ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth")
                : new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "Bearer");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = WebEncoders.Base64UrlDecode(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
    }
}
