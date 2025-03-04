using Microsoft.EntityFrameworkCore;

namespace Employees.Management.Data.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _db;  
        public EmployeeRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Employee employee)
        {           
           await _db.Employees.AddAsync(employee);
            await _db.Employees.AddRangeAsync();
          
        }

        public async Task<Employee?> GetById(string id)
        {
            var employee = await _db.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee;
        }

        public void Delete(Employee employee)
        {
            _db.Employees.Remove(employee);            
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }

}
