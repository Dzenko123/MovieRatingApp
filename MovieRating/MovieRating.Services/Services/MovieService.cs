using AutoMapper;
using MovieRating.Model.SearchObject;
using MovieRating.Services.IServices;

namespace MovieRating.Services.Services
{
    public class MovieService : BaseService<Model.Movie, Database.Movie, MovieSearchObject>, IMovieService
    {
        public MovieService(MovieRatingDBContext context, IMapper mapper) : base(context, mapper) { }
 
    }
}
