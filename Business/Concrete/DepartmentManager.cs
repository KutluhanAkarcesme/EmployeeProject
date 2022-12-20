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
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;
        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public bool Add(Department department)
        {
            bool validation = ValidationTool.Validate(new DepartmentValidator(),department);
            if (validation)
            {
                _departmentDal.Add(department);
                MessageBox.Show("Bölüm Başarıyla Eklendi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
            
        }
        public bool CheckDepartmentUses(int departmentId)
        {
            return _departmentDal.CheckDepartmentUses(departmentId);
        }
        public void Delete(Department department)
        {
            var result = _departmentDal.CheckDepartmentUses(department.Id);
            if (!result) 
            {
                MessageBox.Show("Bu Bölümde Çalışan Personeller Olduğundan Bölüm Silinemiyor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _departmentDal.Delete(department);
            MessageBox.Show("Bölüm Başarıyla Silindi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public Department GetById(int id)
        {
            return _departmentDal.GetById(g => g.Id == id);
        }

        public List<Department> GetList()
        {
            return _departmentDal.GetList();
        }

        public List<DepartmentDto> GetListEmployeeCount()
        {
            return _departmentDal.GetListEmployeeCount();
        }

        public List<Department> GetListWithStatusTrue()
        {
            return _departmentDal.GetListWithStatusTrue();
        }

        public bool StatusChange(Department department)
        {
            _departmentDal.StatusChange(department);
            return true;
        }

        public bool Update(Department department)
        {
            bool validation = ValidationTool.Validate(new DepartmentValidator(), department);
            if (validation)
            {
                _departmentDal.Update(department);
                MessageBox.Show("Bölüm Başarıyla Güncellendi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
    }
}
