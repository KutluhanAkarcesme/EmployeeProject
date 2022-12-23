using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOffDayDal : IEntityRepository<OffDay>
    {
        List<OffDayDto> GetEmployeeOffDays(int employeeId);
        List<OffDayDto> GetAllEmployeeOffDays();
        Employee GetEmployee(int employeeId);
    }
}
