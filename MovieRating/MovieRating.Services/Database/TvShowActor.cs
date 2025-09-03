namespace MovieRating.Services.Database
{
    public class TvShowActor
    {
        public int TvShowId { get; set; }
        public TvShow TvShow { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
