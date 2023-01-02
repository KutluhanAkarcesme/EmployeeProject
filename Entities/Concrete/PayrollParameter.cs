using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PayrollParameter
    {
        public int Id { get; set; }
        public decimal NetMinimumVage { get; set; }
        public decimal GrossMinimumVage { get; set; }
        public decimal Parameter1 { get; set; }
        public decimal Parameter2 { get; set; }

    }
}
