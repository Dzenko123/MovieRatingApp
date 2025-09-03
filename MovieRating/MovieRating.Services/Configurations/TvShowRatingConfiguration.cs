using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRating.Services.Database;

namespace MovieRating.Services.Configurations
{
    public class TvShowRatingConfiguration : BaseConfiguration<TvShowRating>
    {
        public override void Configure(EntityTypeBuilder<TvShowRating> builder)
        {
            base.Configure(builder);

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Value)
                   .IsRequired();

            builder.HasOne(r => r.TvShow)
                   .WithMany(tv => tv.Ratings)
                   .HasForeignKey(r => r.TvShowId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
