using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PayrollParameterManager : IPayrollParameterService
    {
        private readonly IPayrollParameterDal _payrollParameterDal;

        public PayrollParameterManager(IPayrollParameterDal payrollParameterDal)
        {
            _payrollParameterDal = payrollParameterDal;
        }

        public PayrollParameter Get()
        {
            return _payrollParameterDal.GetList().FirstOrDefault();
        }

        public void Update(PayrollParameter payrollParameter)
        {
            var result = Get();
            if (result == null)
            {
                _payrollParameterDal.Add(payrollParameter);
            }
            else
            {
                _payrollParameterDal.Update(payrollParameter);
            }
            
        }
    }
}
