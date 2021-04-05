using Application.Common.Models;
using MediatR;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Application.Features.Users.EventHandlers
{
    public class UserDeletedEventHandler : INotificationHandler<DomainEventNotification<UserDeletedEvent>>
    {
        private readonly ILogger<UserDeletedEventHandler> _logger;
        public UserDeletedEventHandler(ILogger<UserDeletedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(DomainEventNotification<UserDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
