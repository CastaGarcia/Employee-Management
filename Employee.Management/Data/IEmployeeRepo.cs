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
        void Delete(Employee employee);
        Task Update(Employee employee);
        Task SaveChangesAsync();
    }
}
