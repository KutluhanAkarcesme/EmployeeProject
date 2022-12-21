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

        public List<DepartmentDto> GetListEmployeeCount()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = from department in context.Departments
                             select new DepartmentDto
                             {
                                 Id = department.Id,
                                 Name = department.Name,
                                 EmployeeCount = context.Employees.Where(e => e.DepartmentId == department.Id && e.Status != "İşten Ayrıldı").Count()
                             };
                return result.OrderByDescending(p => p.EmployeeCount).ToList();
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
