using Sample.CRUD.Model.DTO;
using Sample.CRUD.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Service.Interface
{
    public interface IUserAuthenticationService
    {
        Task<ServiceResponseModel<LoginResponseModel>> JwtAuthenticate(LoginRequestModel request);
    }
}
