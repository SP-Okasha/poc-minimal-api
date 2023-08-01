using Sample.CRUD.Model.DTO;
using Sample.CRUD.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Service.Interface
{
    public interface IEmployeeService
    {
        Task<ServiceResponseModel<IEnumerable<EmployeResponseModel>>> GetEmployees();
        Task<ServiceResponseModel<EmployeResponseModel>> GetEmployeeById(int id);
        Task<ServiceResponseModel<EmployeResponseModel>> AddEmployee(EmployeeRequestModel request);
        Task<ServiceResponseModel<EmployeResponseModel>> UpdateEmployee(EmployeeRequestModel request, int id);
        Task<ServiceResponseModel<bool>> DeleteEmployee(int id, int deletedBy);
    }
}
