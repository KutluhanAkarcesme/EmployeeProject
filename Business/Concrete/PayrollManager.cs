using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PayrollManager : IPayrollService
    {
        private readonly IPayrollDal _payrollDal;

        public PayrollManager(IPayrollDal payrollDal)
        {
            _payrollDal = payrollDal;
        }

        public bool Add(int mounth, int year)
        {
            int serviceDay = 0;
            var parameter = _payrollDal.GetPayrollParameter();
            var employeeList = _payrollDal.GetEmployeeList(mounth, year);
            foreach (var employee in employeeList)
            {
                int offDays = _payrollDal.GetEmployeeOffDayCount(employee.Id, mounth, year);

                DateTime dateTime1 = Convert.ToDateTime("01." + mounth + "." + year);
                DateTime dateTime2 = dateTime1.AddMonths(1);
                dateTime2 = dateTime2.AddDays(-1);
                if (employee.StartingDate <= dateTime2)
                {
                    if (employee.Status == "İşten Ayrıldı")
                    {
                        if (employee.EndingDate >= dateTime1)
                        {
                            if (employee.EndingDate <= dateTime2)
                            {
                                TimeSpan timeSpan = dateTime2 - employee.StartingDate;
                                if ((Convert.ToInt16(timeSpan.ToString()) + 1) >= dateTime2.Day)
                                {
                                    timeSpan = Convert.ToDateTime(employee.EndingDate) - dateTime1;
                                    serviceDay = Convert.ToInt16(timeSpan.ToString()) + 1;
                                }
                                else
                                {
                                    timeSpan = Convert.ToDateTime(employee.EndingDate) - employee.StartingDate;
                                    serviceDay = Convert.ToInt16(timeSpan.ToString()) + 1;
                                }
                            }
                            else
                            {
                                TimeSpan timeSpan = dateTime2 - employee.StartingDate;
                                if ((Convert.ToInt16(timeSpan.ToString()) + 1) >= dateTime2.Day)
                                {
                                    serviceDay = 30;
                                }
                                else
                                {
                                    serviceDay = Convert.ToInt16(timeSpan.ToString()) + 1;
                                }
                            }
                        }

                    }
                    else
                    {
                        TimeSpan timeSpan = dateTime2 - employee.StartingDate;
                        if ((Convert.ToInt16(timeSpan.ToString()) + 1) >= dateTime2.Day)
                        {
                            serviceDay = 30;
                        }
                        else
                        {
                            serviceDay = Convert.ToInt16(timeSpan.ToString()) + 1;
                        }
                    }
                }

                if (serviceDay > 0)
                {
                    serviceDay = serviceDay - offDays;
                    Payroll payroll = new Payroll
                    {
                        EmployeeId = employee.Id,
                        NetPay = (employee.Salary / 30) * serviceDay,
                        ServiceDay = serviceDay,
                        Mounth = mounth,
                        Year = year,
                        GrossPay = ((employee.Salary / 30) * serviceDay) - ((parameter.Parameter1 / 30) * serviceDay),
                        CumulaticeIncomeTaxAssesment = ((((employee.Salary / 30) * serviceDay) - ((parameter.Parameter1 / 30) * serviceDay)) * parameter.Parameter2) * 15 / 100,
                        IncomeTaxAssesment = ((((employee.Salary / 30) * serviceDay) - ((parameter.Parameter1 / 30) * serviceDay)) * parameter.Parameter2) * 15 / 100,
                        InsurancePremiumEmployeeShare = 0
                    };
                    _payrollDal.Add(payroll);
                }

            }
            return true;
        }

        public void Delete(Payroll payroll)
        {
            _payrollDal.Delete(payroll);
        }

        public List<PayrollDto> GetPayrollListWithEmployee()
        {
            return _payrollDal.GetPayrollListWithEmployee();
        }
    }
}
