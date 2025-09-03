using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRating.Services.Database;

namespace MovieRating.Services.Configurations
{
    public class TvShowActorConfiguration : BaseConfiguration<TvShowActor>
    {
        public override void Configure(EntityTypeBuilder<TvShowActor> builder)
        {
            base.Configure(builder);

            builder.HasKey(ta => new { ta.TvShowId, ta.ActorId });

            builder.HasOne(ta => ta.TvShow)
                   .WithMany(tv => tv.TvShowActors)
                   .HasForeignKey(ta => ta.TvShowId);

            builder.HasOne(ta => ta.Actor)
                   .WithMany(a => a.TvShowActors)
                   .HasForeignKey(ta => ta.ActorId);
        }
    }
}
