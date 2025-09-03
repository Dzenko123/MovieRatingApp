namespace MovieRating.Model
{
    public class MovieRates
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int MovieId { get; set; }
        public string? Movie { get; set; } = null!;
    }


}
