using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniMDB.Domain.Entities;

namespace UniMDB.Infrastructure.Configurations;
public class FavoritoConfiguration : IEntityTypeConfiguration<Favorite>
{

    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(f => f.id_favorite);

        builder
            .HasOne(f => f.user)
            .WithMany(u => u.favorites)
            .HasForeignKey(f => f.id_user)
            .IsRequired();

        builder
            .Property(f => f.id_movie_mdb)
            .IsRequired()
            .HasMaxLength(12)
            .IsFixedLength();
    }

}