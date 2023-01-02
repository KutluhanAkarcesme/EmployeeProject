using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPayrollDal : EfEntityRepositoryBase<Payroll, EmployeeDbContext>, IPayrollDal
    {
        public List<Employee> GetEmployeeList(int mounth, int year)
        {
            using (var context = new EmployeeDbContext())
            {
                DateTime dateTime1 = Convert.ToDateTime("01." + mounth + "." + year);
                DateTime dateTime2 = dateTime1.AddMonths(1);
                dateTime2 = dateTime2.AddDays(-1);

                var result = context.Employees.Where(e => e.StartingDate <= dateTime2 && e.Status != "İşten Ayrıldı" || e.EndingDate >= dateTime1 && e.EndingDate <= dateTime2).ToList();
                return result.OrderBy(e => e.Name).ToList();
            }
        }

        public int GetEmployeeOffDayCount(int employeeId, int mounth, int year)
        {
            using (var context = new EmployeeDbContext())
            {
                DateTime dateTime1 = Convert.ToDateTime("01." + mounth + "." + year);
                DateTime dateTime2 = dateTime1.AddMonths(1);
                dateTime2 = dateTime2.AddDays(-1);
                
                var result = context.OffDays.Where(o => o.EmployeeId == employeeId && o.Date >= dateTime1 && o.Date <= dateTime2).Count();
                return result;
            }
        }

        public List<PayrollDto> GetPayrollListWithEmployee()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = from payroll in context.Payrolls
                             join employee in context.Employees on payroll.EmployeeId equals employee.Id
                             select new PayrollDto
                             {
                                 Id = payroll.Id,
                                 EmployeeId = employee.Id,
                                 Name = employee.Name,
                                 IdentityNumber = employee.IdentityNumber,
                                 CumulaticeIncomeTaxAssesment = payroll.CumulaticeIncomeTaxAssesment,
                                 GrossPay = payroll.GrossPay,
                                 InsurancePremiumEmployeeShare = payroll.InsurancePremiumEmployeeShare,
                                 Mounth = payroll.Mounth,
                                 Year = payroll.Year,
                                 NetPay = payroll.NetPay,
                                 ServiceDay = payroll.ServiceDay,
                             };
                return result.OrderBy(x => x.Name).ToList();
            }
        }

        public PayrollParameter GetPayrollParameter()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.PayrollParameters.FirstOrDefault();
                return result;
            }
        }
    }
}
