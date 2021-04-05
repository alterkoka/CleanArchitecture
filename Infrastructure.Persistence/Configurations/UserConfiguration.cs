using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDCEMS.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ApplicationUserId).IsRequired();
            builder.Property(x => x.IdentityNumber).HasMaxLength(11).IsRequired();
            builder.Property(x => x.Salary).HasColumnType("Money");

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
