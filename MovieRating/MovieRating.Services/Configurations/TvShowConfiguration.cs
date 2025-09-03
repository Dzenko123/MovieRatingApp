using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRating.Services.Database;

namespace MovieRating.Services.Configurations
{
    public class TvShowConfiguration : BaseConfiguration<TvShow>
    {
        public override void Configure(EntityTypeBuilder<TvShow> builder)
        {
            base.Configure(builder);

            builder.HasKey(tv => tv.TvShowId);
        }
    }
}
