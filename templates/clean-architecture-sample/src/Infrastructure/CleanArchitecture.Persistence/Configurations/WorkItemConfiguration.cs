using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.Priority)
                .HasConversion<string>();

            builder.HasIndex(x => new
            {
                x.UserId,
                x.Title
            }).IsUnique();
            builder.HasData(
                new {
                    Id = 1,
                    Title = "First Work Item",
                    Description = "Example",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    Priority = WorkItemPriority.Medium,
                    Status = WorkItemStatus.Pending
                },
                new {
                    Id = 2,
                    Title = "Second Work Item",
                    Description = "Example 2",
                    UserId = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                    Priority = WorkItemPriority.Medium,
                    Status = WorkItemStatus.Pending
                }
            );
        }
    }
}