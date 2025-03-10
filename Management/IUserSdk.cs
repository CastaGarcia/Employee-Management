using Management.Inputs;
using Management.Outputs;
using Refit;

namespace Management
{
    public interface IUserSdk
    {
        private const string BASEURL = "api/users";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreationData"></param>
        /// <returns></returns>
        [Post(BASEURL)]
        Task<UserOutput> Create([Body] UserCreationData userCreationData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get(BASEURL + "{Id}")]
        Task<UserOutput> GetUser(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Delete(BASEURL + "{Id}")]
        Task<UserOutput> DeleteUser(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userUpdateData"></param>
        /// <returns></returns>
        [Put(BASEURL)]
        Task<UserOutput> Update([Body] UserUpdateData userUpdateData);
    }

}