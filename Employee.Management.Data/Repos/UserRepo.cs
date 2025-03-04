using Microsoft.EntityFrameworkCore;

namespace Employees.Management.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _db;
        public UserRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.Users.AddRangeAsync();

        }

        public async Task<User?> GetById(string id)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(e => e.Id == id);

            return user;
        }

        public void Delete(User user)
        {
            _db.Users.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
