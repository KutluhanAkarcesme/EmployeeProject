using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDepartmentDal
    {
        List<Department> GetList();
        Department GetById(int id);
        void Add(Department department);
        void Delete(Department department);
        void Update(Department department);

    }
}
