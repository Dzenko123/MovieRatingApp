namespace MovieRating.Model.SearchObject
{
    public class BaseSearchObject
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? FTS { get; set; }

    }
}
