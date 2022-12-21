using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos
{
    public class OffDayDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
