using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace UsersManagementsEmployees.Web
{
    public class ProveedorAthenticationPrueba : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);
            var usuarioFelipe = new ClaimsIdentity(
            new List<Claim>
            {
                new Claim("llave1", "llave2"),
                new Claim("edad", "555"),
                new Claim(ClaimTypes.Name, "Felipe"),
                new Claim(ClaimTypes.Role, "Admin")
            },
        authenticationType: "prueba");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioFelipe)));
        }
    }

        
}
