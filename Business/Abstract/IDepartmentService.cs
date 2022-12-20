using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDepartmentService
    {
        List<Department> GetList();
        Department GetById(int id);
        bool Add(Department department);
        void Delete(Department department);
        bool Update(Department department);
        bool CheckDepartmentUses(int departmendId);
        bool StatusChange(Department department);
        List<Department> GetListWithStatusTrue();
        List<DepartmentDto> GetListEmployeeCount();
    }
}
