using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.CRUD.Model.Common;
using Sample.CRUD.Model.DTO;
using Sample.CRUD.Model.ResponseModel;
using Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee;
using Sample.CRUD.Repository.EntityFramework.GenericRepository;
using Sample.CRUD.Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Service
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IBaseRepository _genericRepository;
        private readonly ApplicationSettingsModel _appSettings;
        public UserAuthenticationService(IBaseRepository repository, IOptions<ApplicationSettingsModel> options)
        {
            _genericRepository = repository;
            _appSettings = options.Value;
        }

        public async Task<ServiceResponseModel<LoginResponseModel>> JwtAuthenticate(LoginRequestModel request)
        {
            var user = await _genericRepository.GetAsync<ApplicationUser>(x => x.Username == request.Username && x.Userpassword == request.Userpassword && !x.IsDeleted);
            if (user == null)
                return new ServiceResponseModel<LoginResponseModel>("Username or password does not match!", hasValidationError: true);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id",user.Id.ToString()),
                new Claim("Username",user.Username),
                new Claim("RoleId", user.RoleId.ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.Jwt.ValidityInMinutes),
                Issuer = _appSettings.Jwt.Issuer,
                Audience = _appSettings.Jwt.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Jwt.SecretKey)), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ServiceResponseModel<LoginResponseModel>(new LoginResponseModel
            {
                Token = tokenHandler.WriteToken(token),
                ExpiredOn = token.ValidTo
            }, "User authenticated");
        }
    }
}
