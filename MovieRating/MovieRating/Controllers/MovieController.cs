using Microsoft.AspNetCore.Mvc;
using MovieRating.Model;
using MovieRating.Model.SearchObject;
using MovieRating.Services.IServices;

namespace MovieRating.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : BaseController<Model.Movie, MovieSearchObject>
    {
        private readonly IMovieService _movieService;

        public MovieController(ILogger<BaseController<Movie, MovieSearchObject>> logger, IMovieService service) : base(logger, service)
        {
            _movieService = service;

        }

    }
}
