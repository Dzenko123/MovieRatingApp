using MovieRating.Model;

namespace MovieRating.Services
{
    public interface IService<T, TSearch> where TSearch : class
    {
        Task<PagedResult<T>> Get(TSearch? search = null);
        Task<T> GetById(int id);
        Task AddRatingAsync(int id, int value);
    }
}
