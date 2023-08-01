using System;
using System.Collections.Generic;

namespace Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee;

public partial class ApplicationRole
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
}
