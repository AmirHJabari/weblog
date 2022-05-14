using Weblog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weblog.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        // Name
        builder.Property(c => c.Name)
            .HasMaxLength(20)
            .IsRequired();
        
        // Summary
        builder.Property(c => c.Description)
            .HasMaxLength(400)
            .IsRequired();
    }
}
