using Weblog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weblog.Infrastructure.Persistence.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        // Title
        builder.Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();
        
        // Url Title
        builder.Property(b => b.UrlTitle)
            .HasMaxLength(150)
            .IsRequired();
        builder.HasIndex(b => b.UrlTitle)
            .IsUnique();

        // Content
        builder.Property(b => b.Content)
            .HasMaxLength(10000)
            .IsRequired();

        // Summary
        builder.Property(b => b.Summary)
            .HasMaxLength(400)
            .IsRequired();

        // Poster Image File Name
        builder.Property(b => b.PosterImage)
            .HasMaxLength(100)
            .IsRequired();

        // Tag Relations
        builder.HasMany(b => b.TagRelations)
            .WithOne(r => r.From)
            .HasForeignKey(r => r.FromId);
    }
}
