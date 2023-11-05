using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.Name).Must(StartWithA).WithMessage("Belirtilen alan A harfi ile başlamalıdır");
        }

        private bool StartWithA(string param)
        {
            return param.StartsWith("a");
        }
    }
}
