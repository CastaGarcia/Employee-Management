using AutoMapper;
using Employees.Management.Services;
using Management.Inputs;
using Management.Outputs;
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

            return Ok(_mapper.Map<EmployeeOutput>(employee));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            Employee? employee = await _employeeService.GetById(id);

            return Ok(_mapper.Map<EmployeeOutput>(employee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await _employeeService.Delete(id);

            return NoContent();
        }

    }
}
