namespace MovieRating.Model
{
    public class TvShowRating
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int TvShowId { get; set; }
        public TvShow TvShow { get; set; } = null!;
    }

}
