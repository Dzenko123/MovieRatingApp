namespace MovieRating.Services.Database
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte[]? CoverImageUrl { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<MovieRates> Ratings { get; set; } = new List<MovieRates>();
    }
}
