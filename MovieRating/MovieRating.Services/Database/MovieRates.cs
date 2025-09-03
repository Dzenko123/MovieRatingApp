namespace MovieRating.Services.Database
{
    public class MovieRates
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }


}
