using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.ValueObjects;

namespace ExpandMicroservice.Infrastructure.EntityFramework.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasConversion(
                amount => amount.Value,
                value => new Amount(value));

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasConversion(
                description => description == null ? null : description.Value,
                value => value == null ? null : new Description(value))
            .HasMaxLength(300);

        builder.Property(x => x.CreationDate)
            .IsRequired()
            .HasConversion(
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

        builder.Property(x => x.ModificationDate)
            .IsRequired(false)
            .HasConversion(
                src => !src.HasValue ? src : src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                dst => !dst.HasValue ? dst : dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc));

        builder.HasOne(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey("CategoryId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}