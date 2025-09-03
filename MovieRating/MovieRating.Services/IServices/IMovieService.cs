using MovieRating.Model;
using MovieRating.Model.SearchObject;

namespace MovieRating.Services.IServices
{
    public interface IMovieService : IService<Movie, MovieSearchObject>
    {
        Task AddRatingAsync(int movieId, int ratingValue);

    }
}
