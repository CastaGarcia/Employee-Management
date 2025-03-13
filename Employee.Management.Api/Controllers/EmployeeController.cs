using AutoMapper;
using Employees.Management.Models;
using Employees.Management.Services;
using Management;
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
            Employee? employee = await _employeeService.Create(employeeCreationData);
            var result = _mapper.Map<EmployeeOutput>(employee);
            return Ok(result);
        }


        [HttpGet("{id}")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            Employee? employee = await _employeeService.GetById(id);
            var result = _mapper.Map<EmployeeOutput>(employee);
            return Ok(result);
        }

        [HttpGet]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetByFilter([FromQuery] EmployeeGetFilter employeeGetFilter)
        {
            var employees = await _employeeService.GetEmployeesByFilter(employeeGetFilter);
            var result = _mapper.Map<PaginatedListOutput<EmployeeOutput>>(employees);

            return Ok(result);
        }

        
        [HttpDelete("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeService.Delete(id);
            return NoContent();

        }

        [HttpPut]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> Update([FromBody] EmployeeUpdateData employeeUpdateData)
        {
            Employee? employee = await _employeeService.Update(employeeUpdateData);

            return Ok(_mapper.Map<EmployeeOutput>(employee));
        }

    }
}