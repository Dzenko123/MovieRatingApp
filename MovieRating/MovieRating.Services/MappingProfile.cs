using AutoMapper;

namespace MovieRating.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Database.Movie, Model.Movie>();

            CreateMap<Database.MovieActor, Model.MovieActor>();

            CreateMap<Database.Actor, Model.Actor>();

            CreateMap<Database.MovieRates, Model.MovieRates>();

            CreateMap<Database.TvShow, Model.TvShow>();
            CreateMap<Database.TvShowActor, Model.TvShowActor>();
            CreateMap<Database.TvShowRating, Model.TvShowRating>();

            CreateMap<Database.MovieActor, Model.MovieActor>()
            .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title))
            .ForMember(dest => dest.Actor, opt => opt.MapFrom(src => src.Actor));

            CreateMap<Database.MovieRates, Model.MovieRates>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title));

        }
    }
}