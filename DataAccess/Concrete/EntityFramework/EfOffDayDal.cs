﻿using Core.DataAccess.EntityFramework;
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
    public class EfOffDayDal : EfEntityRepositoryBase<OffDay, EmployeeDbContext>, IOffDayDal
    {
        public List<OffDayDto> GetAllEmployeeOffDays()
        {
            using (var context = new EmployeeDbContext())
            {
                var result = from offday in context.OffDays
                             join employee in context.Employees on offday.EmployeeId equals employee.Id
                             select new OffDayDto
                             {
                                 Id = offday.Id,
                                 EmployeeId = offday.EmployeeId,
                                 Name = employee.Name.ToUpper() + " " + employee.LastName.ToUpper(),
                                 Date = offday.Date
                             };
                return result.OrderByDescending(d => d.Date).ToList();
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            using (var context = new EmployeeDbContext())
            {
                var result = context.Employees.Find(employeeId);
                return result;
            }
        }

        public List<OffDayDto> GetEmployeeOffDays(int employeeId)
        {
            using (var context = new EmployeeDbContext())
            {
                var result = from offday in context.OffDays
                             join employee in context.Employees on offday.EmployeeId equals employee.Id
                             where offday.EmployeeId == employeeId
                             select new OffDayDto
                             {
                                 Id = offday.Id,
                                 EmployeeId = offday.EmployeeId,
                                 Name = employee.Name,
                                 Date = offday.Date
                             };
                return result.OrderByDescending(d => d.Date).ToList();  
            }
        }
    }
}
