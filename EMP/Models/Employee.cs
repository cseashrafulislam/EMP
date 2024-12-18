using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<PerformanceReview> PerformanceReviews { get; set; }

    }

}
