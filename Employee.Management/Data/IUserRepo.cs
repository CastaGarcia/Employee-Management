﻿namespace Employees.Management.Data
{
    public interface IUserRepo
    {
        Task AddAsync(User user);
        Task<User?> GetById(string id);
        void Delete(User user);

        Task SaveChangesAsync();
    }
}
