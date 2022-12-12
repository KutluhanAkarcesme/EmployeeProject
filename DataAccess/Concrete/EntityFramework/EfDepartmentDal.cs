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
    public class EfDepartmentDal : IDepartmentDal
    {

        public void Add(Department department)
        {
            using (var context = new EmployeeDbContext())
            {
                context.Departments.Add(department);
                context.SaveChanges();
            }
        }

        public void Delete(Department department)
        {
            using (var context = new EmployeeDbContext())
            {
                context.Departments.Remove(department);
                context.SaveChanges();
            }
        }

        public Department GetById(int id)
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Departments.Where( x => x.Id == id ).FirstOrDefault();
                return result;
            }
        }

        public List<Department> GetList()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Departments.ToList();
                return result;
            }
        }

        public void Update(Department department)
        {
            using(var context = new EmployeeDbContext())
            {
                context.Departments.Update(department);
                context.SaveChanges();
            }
        }
    }
}
