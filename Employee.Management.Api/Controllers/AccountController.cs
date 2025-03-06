using Employees.Management.Api.Helpers;
using Employees.Management.Data;
using Employees.Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Management.Api.Controllers
{
    [Route("api/Tokens")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly JwtSettings _jwtSettings;

        public AccountController(AppDbContext context, JwtSettings jwtSettings)
        {
            _appDbContext = context;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {

            try
            {
                var Token = new UserTokens();

                var searchUser = _appDbContext.Users
                    .FirstOrDefault(user => user.UserName == userLogin.UserName && user.PassWord == userLogin.PassWord);

                if (searchUser == null)
                    return BadRequest("Invalid username or password.");

                Token = JwtHelpers.GenTokenKey(new UserTokens()
                {
                    UserName = searchUser.UserName,
                    PassWord = searchUser.PassWord,
                    Id = searchUser.Id,
                    GuidId = Guid.NewGuid(),
                }, _jwtSettings);

                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }
    }
}
