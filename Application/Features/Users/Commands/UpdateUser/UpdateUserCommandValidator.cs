using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("IdentityNumber is required.")
                .Length(11).WithMessage("IdentityNumber must be 11 characters.");

            RuleFor(x => x.Salary).NotNull().When(x => x.IsEmplyoed);

            RuleFor(s => s.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(s => s.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(s => s.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required");

            RuleFor(s => s.Line)
                .NotEmpty().WithMessage("Line is required");
        }
    }
}
