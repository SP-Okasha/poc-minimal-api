using Microsoft.AspNetCore.Authorization;
using Sample.CRUD.Model.ResponseModel;
using System.Net;

namespace Sample.CRUD.API.MinimalRoutes
{
    public class RouteBase
    {
        public string UrlFragment { get; set; }
        protected ILogger Logger { get; set; }


        public RouteBase(ILogger logger)
        {
            Logger = logger;
        }

        public virtual void AddRoutes(WebApplication app)
        {
        }

        protected async Task<ApiResponseModel> GetResponse<T>(ServiceResponseModel<T> serviceResponse)
        {
            var statusCode = serviceResponse.HasValidationError ? HttpStatusCode.Conflict :
                             serviceResponse.Exception != null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;


            if (statusCode == HttpStatusCode.Conflict)
                AddWarningLog(Logger, serviceResponse.Message);


            if (statusCode == HttpStatusCode.InternalServerError)
                AddErrorLog(Logger, serviceResponse.Message);

            return new ApiResponseModel(
                serviceResponse.HasValidationError ? HttpStatusCode.Conflict :
                             serviceResponse.Exception != null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
                serviceResponse.Message,
                serviceResponse.Data);

        }


        public static void AddErrorLog(ILogger logger, string logMessage)
        {
            logger.LogError(logMessage);
        }

        public static void AddWarningLog(ILogger logger, string logMessage)
        {
            logger.LogWarning(logMessage);

        }
    }
}
