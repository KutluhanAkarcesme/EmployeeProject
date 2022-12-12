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
        public void Delete(Department department)
        {
            _departmentDal.Delete(department);
        }

        public Department GetById(int id)
        {
            return _departmentDal.GetById(id);
        }

        public List<Department> GetList()
        {
            return _departmentDal.GetList();
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
