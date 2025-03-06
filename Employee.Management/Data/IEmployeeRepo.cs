using Employees.Management.Models;

namespace Employees.Management.Data
{
    public interface IEmployeeRepo
    {
        Task AddAsync(Employee employee);
        Task<Employee?> GetById(string id);
        void Delete(Employee employee);
        Task Update(Employee employee);
        Task SaveChangesAsync();
    }
}
