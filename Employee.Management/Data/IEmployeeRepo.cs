using Employees.Management.Models;
using Management;
using Management.Inputs;

namespace Employees.Management.Data
{
    public interface IEmployeeRepo
    {
        Task AddAsync(Employee employee);
        Task<Employee?> GetById(string id);
        Task<PaginatedListOutput<Employee>> GetEmployeesByFilter(EmployeeGetFilter employeeGetFilter);
        Task Delete(string id);
        Task<Employee?> Update(Employee employee);
        Task SaveChangesAsync();
    }
}
