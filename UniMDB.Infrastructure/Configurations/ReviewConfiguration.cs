using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniMDB.Domain.Entities;

namespace UniMDB.Infrastructure.Configurations;
public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{

    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.id_review);

        builder
            .HasOne(r => r.user)
            .WithMany(u => u.reviews)
            .HasForeignKey(r => r.id_review_user)
            .IsRequired();

        builder
            .Property(r => r.id_movie_mdb)
            .IsRequired()
            .HasMaxLength(12)
            .IsFixedLength();

        builder
            .Property(r => r.review)
            .IsRequired();

        builder
            .Property(r => r.comment)
            .IsRequired(false)
            .HasMaxLength(500);

        builder
            .Property(r => r.created_at)
            .IsRequired();
    }

}
