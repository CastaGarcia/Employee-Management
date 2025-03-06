using AutoMapper;
using Employees.Management.Models;
using Employees.Management.Services;
using Management.Inputs;
using Management.Outputs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Management.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeServices employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreationData employeeCreationData)
        {
            try
            {
                Employee? employee = await _employeeService.Create(employeeCreationData);
                var result = _mapper.Map<EmployeeOutput>(employee);
                return Ok(result);


            }
            catch (Exception ex)
            {               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            Employee? employee = await _employeeService.GetById(id);

            return Ok(_mapper.Map<EmployeeOutput>(employee));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await _employeeService.Delete(id);

            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> Update([FromBody] EmployeeUpdateData employeeUpdateData)
        {
            Employee? employee = await _employeeService.Update(employeeUpdateData);

            return Ok(_mapper.Map<EmployeeOutput>(employee));
        }

    }
}
