using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerId { get; set; }
        public decimal Budget { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
