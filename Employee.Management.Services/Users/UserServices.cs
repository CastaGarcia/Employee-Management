using Employees.Management.Data;
using Management.Inputs;

namespace Employees.Management.Services.Users
{
    public class UserServices : IUserServices
    {
        readonly IUserRepo _userRepo;

        public UserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> Create(UserCreationData userCreationData)
        {
            if (string.IsNullOrEmpty(userCreationData.Id) == false)
            {
                User? userExist = await _userRepo.GetById(userCreationData.Id);
                if (userExist != null)
                    throw new Exception("user already exist");
            }

            User user = new User(
                userCreationData.UserName,
                userCreationData.PassWord,
                userCreationData.Id
                );

            await _userRepo.AddAsync(user);

            await _userRepo.SaveChangesAsync();
            return user;
        }

        public async Task Delete(string id)
        {
            User? userExist = await _userRepo.GetById(id);

            if (userExist == null)
                throw new Exception("user doesn't exist");

            _userRepo.Delete(userExist);
            await _userRepo.SaveChangesAsync();
        }

        public async Task<User?> GetById(string id)
        {
            User? userExist = await _userRepo.GetById(id);
            if (userExist == null)
                throw new Exception("the user that you look for doesnt exist");

            return userExist;
        }

        public async Task<User> Update(UserUpdateData userUpdateData)
        {
            if (string.IsNullOrEmpty(userUpdateData.Id))
                throw new Exception("the Id is empty");

            User? UserExist = await _userRepo.GetById(userUpdateData.Id);
            if (UserExist == null)
                throw new Exception("the user that you look for doesnt eist");

            UserExist.Id = userUpdateData.Id;
            UserExist.UserName = userUpdateData.UserName;
            UserExist.PassWord = userUpdateData.PassWord;

            await _userRepo.SaveChangesAsync();
            return UserExist;
        }
    }
}
