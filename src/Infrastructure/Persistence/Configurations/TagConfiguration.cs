using Weblog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weblog.Infrastructure.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        // Title
        builder.Property(t => t.Name)
            .HasMaxLength(20)
            .IsRequired();

        // Blog Relations
        builder.HasMany(b => b.BlogRelations)
            .WithOne(r => r.To)
            .HasForeignKey(r => r.ToId);
    }
}
