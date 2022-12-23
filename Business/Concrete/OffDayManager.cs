using Business.Abstract;
using DataAccess.Abstract;
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
        private readonly IOffDayDal _offDayDal;

        public OffDayManager(IOffDayDal offDayDal)
        {
            _offDayDal = offDayDal;
        }

        public bool Add(int id, string startDate, string endDate)
        {
            DateTime date1 = Convert.ToDateTime(startDate);
            DateTime date2 = Convert.ToDateTime(endDate);
            if (date1 > date2)
            {
                MessageBox.Show("İzin başlangıç tarihi bitiş tarihinden sonra olamaz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            while (date1 <= date2)
            {
                var result = _offDayDal.GetList().Where(e => e.EmployeeId == id).ToList();
                int count = result.Where(o => o.Date == date1).Count();
                if (count > 0)
                {
                    MessageBox.Show("Personel bu tarihler arasında zaten izinli.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                date1 = date1.AddDays(1);
            }
            date1 = Convert.ToDateTime(startDate);
            while (date1 <= date2)
            {
                OffDay offDay = new OffDay()
                {
                    EmployeeId = id,
                    Date = date1
                };
                _offDayDal.Add(offDay);
                date1 = date1.AddDays(1);
            }
            MessageBox.Show("Personel izin kaydı başarıyla gerçekleşti.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        public bool Delete(OffDay offDay)
        {
            var offDayEmployee = _offDayDal.GetEmployee(offDay.EmployeeId);
            if (offDayEmployee.Status != "İşten Ayrıldı")
            {
                _offDayDal.Delete(offDay);
                MessageBox.Show("Personel izni başarıyla silindi","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return true;
            }
            MessageBox.Show("İşten ayrılan personelin izin kaydı silinemez", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public List<OffDayDto> GetAllEmployeeOffDays()
        {
            return _offDayDal.GetAllEmployeeOffDays();
        }

        public OffDay GetById(int id)
        {
            return _offDayDal.GetById(o => o.Id == id);
        }

        public Employee GetEmployee(int employeeId)
        {
            return _offDayDal.GetEmployee(employeeId);
        }

        public OffDay GetEmployeeOffDayByDate(int employeeId, DateTime date)
        {
            return _offDayDal.GetById(g => g.EmployeeId == employeeId && g.Date == date);
        }

        public List<OffDayDto> GetEmployeeOffDays(int employeeId)
        {
            return _offDayDal.GetEmployeeOffDays(employeeId);
        }

        public bool Update(OffDay offDay)
        {
                var result = _offDayDal.GetList().Where(e => e.EmployeeId == offDay.EmployeeId).ToList();
                int count = result.Where(o => o.Date == offDay.Date).Count();
                if (count > 0)
                {
                    MessageBox.Show("Personel bu tarihler arasında zaten izinli.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            _offDayDal.Update(offDay);
            MessageBox.Show("Personel izin tarihi başarıyla güncellendi.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
            
        }
    }
}
