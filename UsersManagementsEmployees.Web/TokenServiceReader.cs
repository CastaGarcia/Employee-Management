using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UsersManagementsEmployees.Web
{       // System.IdentityModel.Tokens.Jwt use for obtaim claim
    public class TokenServiceReader
    {

        private readonly IJSRuntime _jsRuntime;
        public TokenServiceReader(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task<string> GetUsernameFromTokenAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            if(string.IsNullOrEmpty(token))
            {
                return null;
            }

            var jwtHandler = new JwtSecurityTokenHandler(); 
            var jwtToken = jwtHandler.ReadJwtToken(token);

            var usernameClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return usernameClaim;
        }
    }
}
