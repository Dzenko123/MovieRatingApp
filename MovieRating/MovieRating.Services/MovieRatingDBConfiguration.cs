using Microsoft.EntityFrameworkCore;
using MovieRating.Services.Configurations;

namespace MovieRating.Services
{
    public partial class MovieRatingDBContext
    {
        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
        }
    }
}
