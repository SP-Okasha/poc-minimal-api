using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Model.ResponseModel
{
    public class ApiResponseModel
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public ApiResponseModel(HttpStatusCode code, string message, dynamic data = null)
        {
            Success = (int)code >= 200 && (int)code < 300;
            StatusCode = code;
            Message = message;
            Data = data;
        }
    }
}
