using Employees.Management.Data;
using Management.Inputs;

namespace Employees.Management.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        readonly IEmployeeRepo _employeeRepo;

        public EmployeeServices(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<Employee?> Create(EmployeeCreationData employeeCreationData)
        {
            if (string.IsNullOrEmpty(employeeCreationData.Id) == false)
            {
                Employee? employeExist = await _employeeRepo.GetById(employeeCreationData.Id);
                if (employeExist != null)
                    throw new Exception("employe already exist");
            }

            Employee employe = new Employee(
                employeeCreationData.FirstName,
                employeeCreationData.LastName,
                employeeCreationData.Dui,
                employeeCreationData.Id
                );

            await _employeeRepo.AddAsync(employe);

            await _employeeRepo.SaveChangesAsync();
            return employe;
        }

        public async Task Delete(string id)
        {
            Employee? employeExist = await _employeeRepo.GetById(id);

            if (employeExist == null)
                throw new Exception("employe doesnt exist");

            _employeeRepo.Delete(employeExist);
            await _employeeRepo.SaveChangesAsync();
        }

        public async Task<Employee?> GetById(string id)
        {
            Employee? employeExist = await _employeeRepo.GetById(id);
            if (employeExist == null)
                throw new Exception("the employe that you look for doesnt eist");

            return employeExist;
        }
    }
}
