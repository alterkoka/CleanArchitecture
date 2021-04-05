using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PDCEMS.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Country).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.ZipCode).IsRequired();
            builder.Property(x => x.Line).IsRequired();

            builder.HasOne(x => x.User)
                   .WithOne(x => x.Address)
                   .HasForeignKey<Address>(x => x.UserId);
        }
    }
}
