using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Bölüm Adı Boş Geçilemez");
            RuleFor(r => r.Name).MinimumLength(3).WithMessage("Bölüm Adı En Az 3 Karakter Olmalıdır");
            RuleFor(r => r.Name).MaximumLength(50).WithMessage("Bölüm Adı En Fazla 50 Karakter olmalıdır");
        }
    }
}
