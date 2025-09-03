namespace MovieRating.Services.Database
{
    public class TvShow
    {
        public int TvShowId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public byte[]? CoverImageUrl { get; set; }

        public ICollection<TvShowActor> TvShowActors { get; set; } = new List<TvShowActor>();
        public ICollection<TvShowRating> Ratings { get; set; } = new List<TvShowRating>();
    }

}
