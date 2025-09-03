using MovieRating.Model;
using MovieRating.Model.SearchObject;

namespace MovieRating.Services.IServices
{
    public interface ITvShowService : IService<TvShow, TvShowSearchObject>
    {
        Task AddRatingAsync(int tvShowId, int ratingValue);

    }
}
