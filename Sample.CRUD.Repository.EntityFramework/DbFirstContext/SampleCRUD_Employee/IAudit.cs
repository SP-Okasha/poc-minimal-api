using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee
{
    public interface IAudit
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }

    public partial class ApplicationUser : IAudit { }
    public partial class Employee : IAudit { }


}
