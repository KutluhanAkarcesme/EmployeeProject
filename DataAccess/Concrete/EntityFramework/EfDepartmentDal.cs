using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDepartmentDal : EfEntityRepositoryBase<Department,EmployeeDbContext>, IDepartmentDal
    {
        public bool CheckDepartmentUses(int departmentId)
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Employees.Where(e => e.DepartmentId == departmentId).Count();
                return (result > 0 ? false : true);
            }
        }

        public List<Department> GetListWithStatusTrue()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Departments.Where(d => d.Status == true).ToList();
                return result;
            }
        }

        public void StatusChange(Department department)
        {
            if (department.Status)
            {
                department.Status = false;
            }
            else
            {
                department.Status = true;
            }
            using (var context = new EmployeeDbContext())
            {
                context.Departments.Update(department);
                context.SaveChanges();
            }
        }

    }
}
