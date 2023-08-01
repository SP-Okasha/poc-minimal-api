using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.CRUD.Model.Common;
using Sample.CRUD.Model.DTO;
using Sample.CRUD.Model.ResponseModel;
using Sample.CRUD.Service.Interface;

namespace Sample.CRUD.API.MinimalRoutes
{
    public class AuthenticationRoute :RouteBase
    {
        public AuthenticationRoute(ILogger logger):base(logger)
        {
            UrlFragment = "auth";
        }

        public override void AddRoutes(WebApplication app)
        {
            app.MapPost($"{UrlFragment}/jwt",[AllowAnonymous] async ([FromServices] IUserAuthenticationService authenticationService, LoginRequestModel request) => await JwtAuthenticate(authenticationService, request)); ;
        }

        protected async virtual Task<ApiResponseModel> JwtAuthenticate(IUserAuthenticationService authentcationService, LoginRequestModel request)
        {
            return await GetResponse(await authentcationService.JwtAuthenticate(request));
        }
    }
}
