using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class UserUpdatedEvent : DomainEvent
    {
        public UserUpdatedEvent(User item)
        {
            Item = item;
        }

        public User Item { get; }
    }
}
