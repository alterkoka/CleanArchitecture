using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(s => s.Password)
                .NotEmpty().WithMessage("Password is required");

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
