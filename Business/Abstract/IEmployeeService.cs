using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        bool CheckIdentityNumber(string identityNumber);
        List<Employee> GetList();
        Employee GetById(int id);
        bool Add(Employee employee);
        void Delete(Employee employee);
        void Update(Employee employee);
        List<EmployeeDto> GetEmployeeList();


    }
}
