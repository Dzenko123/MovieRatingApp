using Microsoft.AspNetCore.Mvc;
using MovieRating.Model;
using MovieRating.Model.SearchObject;
using MovieRating.Services.IServices;

namespace MovieRating.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TvShowController : BaseController<Model.TvShow, TvShowSearchObject>
    {
        private readonly ITvShowService _tvShowService;

        public TvShowController(ILogger<BaseController<TvShow, TvShowSearchObject>> logger, ITvShowService service) : base(logger, service)
        {
            _tvShowService = service;
        }

    }
}