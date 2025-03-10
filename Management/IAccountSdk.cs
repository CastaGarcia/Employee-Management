using Management.Inputs;
using Management.Outputs;
using Refit;

namespace Management
{
    public interface IAccountSdk
    {
        private const string BASEURL = "api/tokens";

        /// <summary>
        /// Obtener token de autenticación.
        /// </summary>
        /// <param name="userLoged"></param>
        [Post(BASEURL)]
        Task<UserOutput> GetToken([Body] userLogin userLoged);
    }
}
