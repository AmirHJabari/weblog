using Weblog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weblog.Infrastructure.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Ignore(t => t.DomainEvents);
        
        // Name
        builder.Property(t => t.Name)
            .HasMaxLength(20)
            .IsRequired();
        builder.HasIndex(t => t.Name)
            .IsUnique();

        // Blog Relations
        builder.HasMany(t => t.BlogRelations)
            .WithOne(r => r.To)
            .HasForeignKey(r => r.ToId);
    }
}
