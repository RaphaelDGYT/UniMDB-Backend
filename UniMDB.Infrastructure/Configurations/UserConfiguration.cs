using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniMDB.Domain.Entities;

namespace UniMDB.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users")
            .HasKey(u => u.id_user);

        builder
            .HasIndex(u => u.email)
            .IsUnique();

        builder
            .HasIndex(u => u.username)
            .IsUnique();


        builder
            .HasMany(u => u.reviews)
            .WithOne(r => r.user)
            .HasForeignKey(r => r.id_review_user)
            .IsRequired(false);


        builder.Property(u => u.name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.email) 
            .IsRequired()
            .HasMaxLength(255)
            .IsConcurrencyToken();

        builder.Property(u => u.username)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.password)
            .IsRequired()
            .HasMaxLength(255);

    }

}
