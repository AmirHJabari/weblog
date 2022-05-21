using Weblog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weblog.Infrastructure.Persistence.Configurations;

public class BlogToTagRelationConfiguration : IEntityTypeConfiguration<BlogToTagRelation>
{
    public void Configure(EntityTypeBuilder<BlogToTagRelation> builder)
    {
        builder.HasKey(x => x.ToId);
        builder.HasKey(x => x.FromId);
    }
}
