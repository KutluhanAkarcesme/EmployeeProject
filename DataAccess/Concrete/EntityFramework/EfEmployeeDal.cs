using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeDal : EfEntityRepositoryBase<Employee,EmployeeDbContext>, IEmployeeDal
    {
        public bool CheckIdentityNumber(string identityNumber)
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Employees.Where(e => e.IdentityNumber == identityNumber);
                return (result.Count() > 0 ? false : true);
            }
        }
    }
}
