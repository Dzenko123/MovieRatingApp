using AutoMapper;
using MovieRating.Model.SearchObject;
using MovieRating.Services.IServices;

namespace MovieRating.Services.Services
{
    public class TvShowService : BaseService<Model.TvShow, Database.TvShow, TvShowSearchObject>, ITvShowService
    {
        public TvShowService(MovieRatingDBContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }
}
