using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPayrollDal : IEntityRepository<Payroll>
    {
        List<PayrollDto> GetPayrollListWithEmployee();
        List<Employee> GetEmployeeList();
        List<PayrollListDto> GetPayrollList();
        PayrollParameter GetPayrollParameter();
        int GetEmployeeOffDayCount(int employeeId, int mounth , int year);
    }
}
