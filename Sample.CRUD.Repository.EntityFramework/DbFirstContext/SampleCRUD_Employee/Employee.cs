using System;
using System.Collections.Generic;

namespace Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmployeeCode { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ApplicationUser UpdatedByNavigation { get; set; } = null!;
}
