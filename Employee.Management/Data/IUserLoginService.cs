using Employees.Management.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Management.Data
{
    public interface IUserLoginService
    {
        Task<(bool IsUserRegistered, string Message)> RegisterNewUser(UsingRegistrationDto userRegistrationDto);
    }
}
