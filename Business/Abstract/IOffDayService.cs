using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOffDayService 
    {
        bool Add(int id,string startDate,string endDate);
        void Update(OffDay offDay);
        void Delete(OffDay offDay);
        List<OffDayDto> GetEmployeeOffDays(int employeeId);
        OffDay GetEmployeeOffDayByDate(int employeeId,DateTime date);
    }
}
