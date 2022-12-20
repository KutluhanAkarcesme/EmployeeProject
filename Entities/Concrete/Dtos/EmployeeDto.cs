using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Decimal Salary { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string ReasonOfLeaving { get; set; }
        public string IdentityNumber { get; set; }
        public string Status { get; set; }
    }
}
