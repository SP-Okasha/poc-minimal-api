using System;
using System.Collections.Generic;

namespace Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
