using Business.Abstract;
using Business.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public bool Add(Employee employee)
        {
            bool validation = ValidationTool.Validate(new EmployeeValidator(), employee);
            if (validation)
            {
                var result = _employeeDal.ChechkIdentityNumber(employee.IdentityNumber);
                if (result > 0)
                {
                    MessageBox.Show("Bu T.C Kimlik Numarasına Kayıtlı Bir Personel Var!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _employeeDal.Add(employee);
                MessageBox.Show("Personel Başarıyla Eklendi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
            
        }

        public int CheckedIdentityNumber(string identityNumber)
        {
            return _employeeDal.ChechkIdentityNumber(identityNumber);
        }

        public void Delete(Employee employee)
        {
            _employeeDal.Delete(employee);
        }

        public Employee GetById(int id)
        {
            return _employeeDal.GetById(id);
        }

        public List<Employee> GetList()
        {
            return _employeeDal.GetList();
        }

        public void Update(Employee employee)
        {
            _employeeDal.Update(employee);
        }
    }
}
