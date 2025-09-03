using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRating.Services.Database;

namespace MovieRating.Services.Configurations
{
    public class MovieActorConfiguration : BaseConfiguration<MovieActor>
    {
        public override void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            base.Configure(builder);

            builder.HasKey(ma => new { ma.MovieId, ma.ActorId });

            builder.HasOne(ma => ma.Movie)
                   .WithMany(m => m.MovieActors)
                   .HasForeignKey(ma => ma.MovieId);

            builder.HasOne(ma => ma.Actor)
                   .WithMany(a => a.MovieActors)
                   .HasForeignKey(ma => ma.ActorId);
        }
    }
}
