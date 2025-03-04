using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Management
{
    public class UserTokens
    {        
        public string Id { get; set; }
        public string Token { get; set; } 
        public string UserName { get; set; }
        public TimeSpan Validity { get; set; } 
        public string RefreshToken { get; set; }
        public string PassWord { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
