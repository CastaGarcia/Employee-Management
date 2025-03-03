using Employees.Management;
using Employees.Management.Data;

namespace Employees.Management.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _db;
        public UserRepo(AppDbContext db)
        {
            _db = db;
        }

        public Task<User> CreateUser(string id, string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
