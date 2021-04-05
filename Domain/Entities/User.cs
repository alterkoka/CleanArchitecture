using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity<Guid>, IHasDomainEvent
    {
        public string ApplicationUserId { get; set; }
        public string IdentityNumber { get; set; }
        public bool Single { get; set; }
        public bool IsEmplyoed { get; set; }
        public decimal? Salary { get; set; }
        public Address Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
