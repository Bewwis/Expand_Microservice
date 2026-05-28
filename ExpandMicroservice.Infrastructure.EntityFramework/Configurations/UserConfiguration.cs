using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpandMicroservice.Domain.Entities;
using ExpandMicroservice.ValueObjects;
using ExpandMicroservice.ValueObjects.Validators;

namespace ExpandMicroservice.Infrastructure.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasConversion(
                username => username.Value,
                value => new Username(value))
            .HasMaxLength(UsernameValidator.MaxLength);

        
    }
}