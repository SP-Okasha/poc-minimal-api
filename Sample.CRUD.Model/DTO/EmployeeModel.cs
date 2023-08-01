using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Model.DTO
{

    public class EmployeResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public int DepartmentId { get; set; }
        public int CreatedBy { get; set; }
    }
}
