using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace UsersManagementsEmployees.Web
{
    public class ProveedorAthenticationPrueba : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public ProveedorAthenticationPrueba(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            ClaimsIdentity identity;

            if (string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity();
            }
            else
            {
                // Si tienes un token válido, extrae el nombre de usuario desde el token
                var claims = ParseClaimsFromJwt(token);
                var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                identity = new ClaimsIdentity(claims, "Bearer");
            }

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        
           /* await Task.Delay(3000);
            var usuarioFelipe = new ClaimsIdentity(
            new List<Claim>
            {
                new Claim("llave1", "llave2"),
                new Claim("edad", "555"),
                new Claim(ClaimTypes.Name, "Felipe"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "prueba");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioFelipe)));*/
        }
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = WebEncoders.Base64UrlDecode(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
        public void UserIsAuthenticated(string username)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
            }, "Bearer");

            var user = new ClaimsPrincipal(identity);
            var autenticationState = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(autenticationState));
        }
        // Método adicional para marcar al usuario como desconectado (cuando hace logout).
        public void MarkUserAsLoggedOut()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }
    }

        
}
