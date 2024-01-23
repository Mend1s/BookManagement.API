using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Persistence.Configurations;

public class LoansConfigurations : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder
            .HasKey(l => l.Id);

        builder
            .HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.IdUser);

        builder
            .HasOne(l => l.Book)
            .WithMany()
            .HasForeignKey(l => l.IdBook);
    }
}
