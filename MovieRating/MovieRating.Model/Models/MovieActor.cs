namespace MovieRating.Model
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public string? Movie { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
