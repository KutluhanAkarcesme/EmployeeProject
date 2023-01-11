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
        public List<Employee> GetEmployeeList()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Employees.ToList();
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

        public List<PayrollListDto> GetPayrollList()
        {
            using (var context = new EmployeeDbContext())
            {
                var f = from x in context.Payrolls.OrderBy(o => o.Mounth).ToList()
                        group x.Mounth by x.Mounth into g
                        select new { Id = g.Key, PayrollList = g.ToList() };

                var result = from x in f.ToList()
                             select new PayrollListDto
                             {
                                 Mounth = x.PayrollList.FirstOrDefault(),
                                 Year = 2023 ,
                                 EmployeeCount = context.Payrolls.Where(p => p.Mounth == x.PayrollList.FirstOrDefault() && p.Year == 2023).Count(),
                                 TotalNetPay = context.Payrolls.Where(p => p.Mounth == x.PayrollList.FirstOrDefault() && p.Year == 2023).Sum(s => s.NetPay)
                             };
                return result.ToList();
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
                                 Name = employee.Name.ToUpper() + " " + employee.LastName.ToUpper(),
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
                var result = context.PayrolParameters.FirstOrDefault();
                return result;
            }
        }
    }
}
