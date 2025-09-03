namespace MovieRating.Services.Database
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<TvShowActor> TvShowActors { get; set; } = new List<TvShowActor>();
    }
}
