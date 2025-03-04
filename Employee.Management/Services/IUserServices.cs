using Management.Inputs;

namespace Employees.Management.Services
{
    public interface IUserServices
    {
        Task<User?> Create(UserCreationData userCreationData);

        Task<User?> GetById(string id);

        Task Delete(string id);

        Task<User?> Update(UserUpdateData userUpdateData);
    }
}
