using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Management
{
    public class UserTokens
    {
        //ESTA ES INFORMACION QUE ENVIAMOS AL USUARIO UNA VES HAGA LOGIN
        public int Id { get; set; }
        public string Token { get; set; } //entrega el token al usuario
        public string UserName { get; set; }
        public TimeSpan Validity { get; set; } //tiempo que dura el token
        public string RefreshToken { get; set; }
        public string EmailId { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
