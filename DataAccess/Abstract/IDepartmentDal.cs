using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDepartmentDal : IEntityRepository<Department>
    {
        bool CheckDepartmentUses(int departmentId);
        void StatusChange(Department department);
        List<Department> GetListWithStatusTrue();
        List<DepartmentDto> GetListEmployeeCount();
    }
}
