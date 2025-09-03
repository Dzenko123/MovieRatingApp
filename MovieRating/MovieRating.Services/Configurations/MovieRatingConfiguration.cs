using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRating.Services.Database;

namespace MovieRating.Services.Configurations
{
    public class MovieRatingConfiguration : BaseConfiguration<MovieRates>
    {
        public override void Configure(EntityTypeBuilder<MovieRates> builder)
        {
            base.Configure(builder);

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Value)
                   .IsRequired();

            builder.HasOne(r => r.Movie)
                   .WithMany(m => m.Ratings)
                   .HasForeignKey(r => r.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
