using Management.Inputs;

namespace Employees.Management.Services
{
    public interface IEmployeeServices
    {
        Task<Employee?> Create(EmployeeCreationData employeeCreationData);

        Task<Employee?> GetById(string id);

        Task Delete(string id);
    }
}
