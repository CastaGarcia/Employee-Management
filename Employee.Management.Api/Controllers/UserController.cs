using AutoMapper;
using Employees.Management.Services;
using Management.Inputs;
using Management.Outputs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Management.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public UserController(IUserServices userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreationData userCreationData)
        {
            User? user = await _userService.Create(userCreationData);

            return Ok(_mapper.Map<UserOutput>(user));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            User? user = await _userService.GetById(id);

            return Ok(_mapper.Map<UserOutput>(user));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.Delete(id);

            return NoContent();
        }
    }
}
