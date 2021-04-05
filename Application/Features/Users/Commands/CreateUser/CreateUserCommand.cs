using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IdentityNumber { get; set; }
        public bool Single { get; set; }
        public bool IsEmplyoed { get; set; }
        public decimal? Salary { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserId) =  await _identityService.CreateUserAsync(request.UserName, request.Password, request.Email);
            if (!Result.Succeeded)
                throw new ValidationException(nameof(IIdentityService.CreateUserAsync), Result.Errors);

            var entity = new User
            {
                ApplicationUserId = UserId,
                IdentityNumber = request.IdentityNumber,
                Single = request.Single,
                IsEmplyoed = request.IsEmplyoed,
                Salary = request.IsEmplyoed ? request.Salary : null,
                DateCreated = DateTime.Now,
                Address = new Address
                {
                    Country = request.Country,
                    City = request.City,
                    ZipCode = request.ZipCode,
                    Line = request.Line
                }
            };

            entity.DomainEvents.Add(new UserCreatedEvent(entity));

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
