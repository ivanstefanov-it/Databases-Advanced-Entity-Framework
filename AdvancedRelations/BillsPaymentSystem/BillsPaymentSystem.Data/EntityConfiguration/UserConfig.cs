using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillsPaymentSystem.Data.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(f => f.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(l => l.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Email)
                .IsUnicode(false)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.Password)
                .IsUnicode(false)
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}
