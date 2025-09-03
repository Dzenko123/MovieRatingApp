namespace MovieRating.Infrastructure
{
    public class JwtOptions
    {
        public required string JwtSecret { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int ExpirationTimeInDays { get; set; }
        public required int RefreshTokenExpirationTimeInDays { get; set; }
        public required int RefreshTokenLength { get; set; }
    }
}
