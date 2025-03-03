namespace Employees.Management.Data
{
    public interface IUserRepo
    {
        Task<User> CreateUser(string id, string username, string password);
    }
}
