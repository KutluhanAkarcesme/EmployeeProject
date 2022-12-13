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
    public class EfEmployeeDal : IEmployeeDal
    {
        public void Add(Employee employee)
        {
            using (var context = new EmployeeDbContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }

        public void Delete(Employee employee)
        {
            using (var context = new EmployeeDbContext())
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
        }

        public Employee GetById(int id)
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Employees.Where(x => x.Id == id).FirstOrDefault();
                return result;
            }
        }

        public List<Employee> GetList()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Employees.ToList();
                return result;
            }
        }

        public void Update(Employee employee)
        {
            using (var context = new EmployeeDbContext())
            {
                context.Employees.Update(employee);
                context.SaveChanges();
            }
        }
        public int ChechkIdentityNumber(string identityNumber)
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Employees.Where(e => e.IdentityNumber == identityNumber);
                return result.Count();
            }
        }
    }
}
