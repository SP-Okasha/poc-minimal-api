using Microsoft.AspNetCore.Authorization;
using Sample.CRUD.Model.ResponseModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Sample.CRUD.API.Extension
{
    public class JwtAuthenticationExtension
    {
        private readonly RequestDelegate _next;

        public JwtAuthenticationExtension(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            //Check if "AllowAnonymous" is present or not
            if (context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }

            if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
            {
                await GetUnAuthorizedRequest(context, "Unauthorized Access");
                return;
            }

            var token = context.Request.Headers["Authorization"].ToString().Substring(7);//Removing "Bearer " from value

            var securityToken = new JwtSecurityTokenHandler().ReadToken(token);
            if (securityToken.ValidTo < DateTime.UtcNow)
            {
                //token expired
                context = await GetUnAuthorizedRequest(context, "Your token has been expired!");
                return;
            }   


            await _next(context);
        }


        private async Task<HttpContext> GetUnAuthorizedRequest(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            var response = new ApiResponseModel(System.Net.HttpStatusCode.Unauthorized, message);
            
            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            return context;

        }
    }
}
