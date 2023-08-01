using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Model.ResponseModel
{
    public class ServiceResponseModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess => Exception == null && !HasValidationError;
        public bool HasValidationError { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }

        public ServiceResponseModel(T data, string message)
        {
            Data = data;
            Message = message;
            Exception = null;
            HasValidationError = false;
        }


        public ServiceResponseModel(Exception ex)
        {
            Exception = ex;
            Message = ex.Message;
        }


        public ServiceResponseModel(string message, bool hasValidationError = false)
        {
            Message = message;
            HasValidationError = hasValidationError;
        }
    }
}
