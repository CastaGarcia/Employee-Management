using Employees.Management.Models;
using Management;
using Management.Inputs;

namespace Employees.Management.Services
{
    public interface IEmployeeServices
    {
        Task<Employee?> Create(EmployeeCreationData employeeCreationData);

        Task<Employee?> GetById(string id);

        Task<PaginatedListOutput<Employee?>> GetEmployeesByFilter(EmployeeGetFilter employeeGetFilter);

        Task Delete(string id);

        Task<Employee?> Update(EmployeeUpdateData EmployeeUpdateData);      
    }
}
