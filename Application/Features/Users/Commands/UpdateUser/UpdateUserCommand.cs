using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; }
        public bool Single { get; set; }
        public bool IsEmplyoed { get; set; }
        public decimal? Salary { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.Include(x=>x.Address).SingleOrDefaultAsync(x =>x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Users), request.Id);
            }

            entity.IdentityNumber = request.IdentityNumber;
            entity.Single = request.Single;
            entity.IsEmplyoed = request.IsEmplyoed;
            entity.Salary = request.IsEmplyoed ? request.Salary : null;
            entity.Address.Country = request.Country;
            entity.Address.City = request.City;
            entity.Address.ZipCode = request.ZipCode;
            entity.Address.Line = request.Line;

            entity.DomainEvents.Add(new UserUpdatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
