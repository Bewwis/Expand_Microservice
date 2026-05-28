using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.ValueObjects;
using ExpandMicroservice.ValueObjects.Validators;

namespace ExpandMicroservice.Infrastructure.EntityFramework.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasConversion(
                name => name.Value,
                value => new CategoryName(value))
            .HasMaxLength(CategoryNameValidator.MaxLength);

        builder.HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}