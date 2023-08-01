using Sample.CRUD.Model.DTO;
using Sample.CRUD.Model.ResponseModel;
using Sample.CRUD.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Sample.CRUD.API.MinimalRoutes
{
    public class EmployeeRoute : RouteBase
    {
        public EmployeeRoute(ILogger logger) : base(logger)
        {
            UrlFragment = "employee";
        }


        public override void AddRoutes(WebApplication app)
        {
            app.MapGet($"{UrlFragment}", async ([FromServices] IEmployeeService employeeService) => await GetEmployees(employeeService)); ;
            app.MapGet($"{UrlFragment}/{{id:int}}", async ([FromServices] IEmployeeService employeeService, int id) => await GetEmployeeById(employeeService, id));
            app.MapPost($"{UrlFragment}", async ([FromServices] IEmployeeService employeeService, EmployeeRequestModel request) => await AddEmployee(employeeService, request));
            app.MapPut($"{UrlFragment}", async ([FromServices] IEmployeeService employeeService, EmployeeRequestModel request, int id) => await UpdateEmployee(employeeService, request, id));
            app.MapDelete($"{UrlFragment}/{{id:int}}", async ([FromServices] IEmployeeService employeeService, int id, int deletedBy) => await DeleteEmployee(employeeService, id, deletedBy));
        }



        protected async virtual Task<ApiResponseModel> GetEmployees(IEmployeeService employeeService)
        {
            return await GetResponse(await employeeService.GetEmployees());
        }


        protected async virtual Task<ApiResponseModel> GetEmployeeById(IEmployeeService employeeService, int id)
        {
            return await GetResponse(await employeeService.GetEmployeeById(id));
        }


        protected async virtual Task<ApiResponseModel> AddEmployee(IEmployeeService employeeService, EmployeeRequestModel request)
        {
            return await GetResponse(await employeeService.AddEmployee(request));
        }


        protected async virtual Task<ApiResponseModel> UpdateEmployee(IEmployeeService employeeService, EmployeeRequestModel request, int id)
        {
            return await GetResponse(await employeeService.UpdateEmployee(request, id));
        }


        protected async virtual Task<ApiResponseModel> DeleteEmployee(IEmployeeService employeeService, int id, int deletedBy)
        {
            return await GetResponse(await employeeService.DeleteEmployee(id, deletedBy));
        }
    }
}
