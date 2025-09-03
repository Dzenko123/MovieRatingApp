using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRating.Services.Database;

namespace MovieRating.Services.Configurations
{
    public class MovieConfiguration : BaseConfiguration<Movie>
    {
        public override void Configure(EntityTypeBuilder<Movie> builder)
        {
            base.Configure(builder);

            builder.HasKey(m => m.MovieId);
        }
    }
}
