using Business.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OffDayManager : IOffDayService
    {
        private readonly IOffDayService _offDayService;

        public OffDayManager(IOffDayService offDayService)
        {
            _offDayService = offDayService;
        }

        public void Add(OffDay offDay)
        {
            _offDayService.Add(offDay);
        }

        public void Delete(OffDay offDay)
        {
            _offDayService.Delete(offDay);
        }

        public List<OffDayDto> GetEmployeeOffDays(int employeeId)
        {
            return _offDayService.GetEmployeeOffDays(employeeId);
        }

        public void Update(OffDay offDay)
        {
            _offDayService.Update(offDay);
        }
    }
}
