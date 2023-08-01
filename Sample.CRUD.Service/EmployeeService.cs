using Sample.CRUD.Model.DTO;
using Sample.CRUD.Model.ResponseModel;
using Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee;
using Sample.CRUD.Repository.EntityFramework.GenericRepository;
using Sample.CRUD.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository _genericRepository;
        public EmployeeService(IBaseRepository repository)
        {
            _genericRepository = repository;
        }


        public async Task<ServiceResponseModel<EmployeResponseModel>> AddEmployee(EmployeeRequestModel request)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmployeeCode = request.EmployeeCode,
                DepartmentId = request.DepartmentId
            };

            await _genericRepository.AddAsync(employee, request.CreatedBy);

            return new ServiceResponseModel<EmployeResponseModel>((await GetEmployeeById(employee.Id)).Data, "Employee added successfully");

        }

        public async Task<ServiceResponseModel<bool>> DeleteEmployee(int id, int deletedBy)
        {
            var employee = await _genericRepository.GetAsync<Employee>(x => !x.IsDeleted && x.Id == id);
            await _genericRepository.Remove(employee, deletedBy);
            return new ServiceResponseModel<bool>(await _genericRepository.SaveChangesAsync(), "Employee deleted successfully");
        }

        public async Task<ServiceResponseModel<EmployeResponseModel>> GetEmployeeById(int id)
        {
            var employee = await _genericRepository.GetAsync<Employee>(x => !x.IsDeleted && x.Id == id, D => D.Department);
            var response = new EmployeResponseModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmployeeCode = employee.EmployeeCode,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.DepartmentName,
            };
            return new ServiceResponseModel<EmployeResponseModel>(response, "User details");
        }

        public async Task<ServiceResponseModel<IEnumerable<EmployeResponseModel>>> GetEmployees()
        {
            try
            {

                var employees = await _genericRepository.GetAllAsync<Employee>(x => !x.IsDeleted, D => D.Department);
                var response = new List<EmployeResponseModel>();
                foreach (var employee in employees)
                {

                    response.Add(new EmployeResponseModel
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        EmployeeCode = employee.EmployeeCode,
                        DepartmentId = employee.DepartmentId,
                        DepartmentName = employee.Department.DepartmentName,
                    });
                }
                return new ServiceResponseModel<IEnumerable<EmployeResponseModel>>(response, "Employees");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResponseModel<EmployeResponseModel>> UpdateEmployee(EmployeeRequestModel request,int id)
        {
            var employee = await _genericRepository.GetAsync<Employee>(x => !x.IsDeleted && x.Id == id);
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.EmployeeCode = request.EmployeeCode;
            employee.DepartmentId = request.DepartmentId;

            await _genericRepository.Update(employee, request.CreatedBy);
            return new ServiceResponseModel<EmployeResponseModel>((await GetEmployeeById(employee.Id)).Data, "Employee updated successfully");
        }
    }
}
