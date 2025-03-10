using Management.Inputs;
using Management.Outputs;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management
{
    public interface IAccountSdk
    {
        private const string BASEURL = "api/tokens";

        /// <summary>
        /// Obtener token de autenticación.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns>Token de autenticación</returns>
        [Post(BASEURL)]
        Task<UserOutput> GetToken([Body] userLogin userLoged);
    }
}
