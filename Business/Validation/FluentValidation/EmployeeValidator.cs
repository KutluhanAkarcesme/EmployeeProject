using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Personel Adı Boş Bırakılamaz.");
            RuleFor(r => r.Name).MinimumLength(3).WithMessage("Personel Adı 3 Karakterden Uzun Olmalı.");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Personel Soyadı Boş Bırakılamaz.");
            RuleFor(r => r.LastName).MinimumLength(3).WithMessage("Personel Soyadı 3 Karakterden Uzun Olmalı.");
            RuleFor(r => r.DepartmentId).GreaterThan(0).WithMessage("Lütfen Bölüm Seçiniz");
            RuleFor(r => r.BirthDate).LessThan(DateTime.Now.AddYears(-18)).WithMessage("Personel Yaşı 18 den Küçük Olamaz");
            RuleFor(r => r.IdentityNumber).Length(11).WithMessage("T.C Kimlik Numarası 11 Hane Olmalıdır ve Boş Bırakılamaz");
        }
    }
}
