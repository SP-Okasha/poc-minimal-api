using System;
using System.Collections.Generic;

namespace Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee;

public partial class ApplicationUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Userpassword { get; set; } = null!;

    public int RoleId { get; set; }

    public int IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Employee> EmployeeCreatedByNavigations { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> EmployeeUpdatedByNavigations { get; set; } = new List<Employee>();

    public virtual ICollection<ApplicationUser> InverseCreatedByNavigation { get; set; } = new List<ApplicationUser>();

    public virtual ICollection<ApplicationUser> InverseUpdatedByNavigation { get; set; } = new List<ApplicationUser>();

    public virtual ApplicationRole Role { get; set; } = null!;

    public virtual ApplicationUser UpdatedByNavigation { get; set; } = null!;
}
