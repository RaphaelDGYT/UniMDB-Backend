using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniMDB.Domain.Entities;

namespace UniMDB.Infrastructure.Configurations;

//  Todas as configurações envolvendo a tabela 'Users', ou seja, os relacionamentos, valores defaults e etc

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.id_user);

        builder
            .HasMany(u => u.reviews)
            .WithOne(r => r.user)
            .HasForeignKey(r => r.id_review_user)
            .IsRequired(false);

        builder.Property(u => u.name)
            .IsRequired();
        builder.Property(u => u.email) 
            .IsRequired();
        builder.Property(u => u.password)
            .IsRequired();
        builder.Property(u => u.username)
            .IsRequired();
    }

}
