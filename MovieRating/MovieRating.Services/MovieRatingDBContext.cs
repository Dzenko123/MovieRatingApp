using Microsoft.EntityFrameworkCore;
using MovieRating.Services.Database;

namespace MovieRating.Services {
    public partial class MovieRatingDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<MovieRates> MovieRates { get; set; }
        public DbSet<TvShow> TvShow { get; set; }
        public DbSet<TvShowActor> TvShowActor { get; set; }
        public DbSet<TvShowRating> TvShowRating { get; set; }

        public MovieRatingDBContext(DbContextOptions<MovieRatingDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);

            ApplyConfigurations(modelBuilder);
        }
    }
}