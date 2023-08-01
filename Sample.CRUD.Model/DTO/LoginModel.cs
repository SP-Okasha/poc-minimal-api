using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Model.DTO
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Userpassword { get; set; }
    }

    public class LoginResponseModel
    {
        public string Token { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
