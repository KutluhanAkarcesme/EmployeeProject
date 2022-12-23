using Business.Abstract;
using Business.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Concrete.Dtos;
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
                var result = _employeeDal.CheckIdentityNumber(employee.IdentityNumber);
                if (!result)
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

        public bool CheckIdentityNumber(string identityNumber)
        {
            return _employeeDal.CheckIdentityNumber(identityNumber);
        }

        public void Delete(Employee employee)
        {
            _employeeDal.Delete(employee);
        }

        public Employee GetById(int id)
        {
            return _employeeDal.GetById(g => g.Id == id);
        }

        public List<EmployeeDto> GetEmployeeList()
        {
            return _employeeDal.GetEmployeeList().ToList();
        }

        public List<OffDayEmployeeDto> GetEmployeeListByOffDay()
        {
            return _employeeDal.GetEmployeeListByOffDay().ToList();
        }

        public List<Employee> GetList()
        {
            return _employeeDal.GetList();
        }

        public void QuitJob(Employee employee)
        {
            var result = _employeeDal.GetOffDayByEmployee(employee.Id, Convert.ToDateTime(employee.EndingDate));
            if (result != null)
            {
                MessageBox.Show("İzinli personel işten çıkartılamaz", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _employeeDal.Update(employee);
            MessageBox.Show("Personel başarıyla işten çıkartıldı", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ReHired(Employee employee)
        {
            _employeeDal.Update(employee);
            MessageBox.Show("Personel girişi başarıyla gerçekleşti", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool Update(Employee employee)
        {
            bool validation = ValidationTool.Validate(new EmployeeValidator(), employee);
            if (validation)
            {
                var findEmployee = _employeeDal.GetById(i => i.Id == employee.Id);
                var result = true;
                if (findEmployee.IdentityNumber != employee.IdentityNumber) 
                {
                    result = _employeeDal.CheckIdentityNumber(employee.IdentityNumber);
                    if (!result)
                    {
                        MessageBox.Show("Bu T.C Kimlik Numarasına Kayıtlı Bir Personel Var!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                _employeeDal.Update(employee);
                MessageBox.Show("Personel Başarıyla Güncellendi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }

        public void UpdateList(Employee employee)
        {
            _employeeDal.Update(employee);
        }
    }
}
