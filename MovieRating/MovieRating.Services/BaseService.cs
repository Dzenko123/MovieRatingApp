using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieRating.Model;
using MovieRating.Model.SearchObject;
using System.Text.RegularExpressions;

namespace MovieRating.Services
{
    public class BaseService<T, TDb, TSearch> : IService<T, TSearch>
     where TDb : class
     where T : class
     where TSearch : BaseSearchObject
    {
        protected MovieRatingDBContext _context;
        protected IMapper _mapper { get; set; }

        public BaseService(MovieRatingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<PagedResult<T>> Get(TSearch? search = null)
        {
            var query = _context.Set<TDb>().AsQueryable();

            query = AddInclude(query, search);
            query = AddFilter(query, search);
            query = AddSort(query, search);

            PagedResult<T> result = new PagedResult<T>();
            result.Count = await query.CountAsync();

            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
            {
                query = query.Skip(search.Page.Value * search.PageSize.Value)
                             .Take(search.PageSize.Value);
            }

            var list = await query.ToListAsync();
            result.Result = _mapper.Map<List<T>>(list);
            return result;
        }

        public virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query, TSearch? search = null)
        {
            if (typeof(TDb) == typeof(Database.Movie))
            {
                return query
                    .Include("MovieActors.Actor")
                    .Include("Ratings");
            }
            else if (typeof(TDb) == typeof(Database.TvShow))
            {
                return query
                    .Include("TvShowActors.Actor")
                    .Include("Ratings");
            }

            return query;
        }


        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch? search = null)
        {
            if (search == null || string.IsNullOrWhiteSpace(search.FTS))
                return query;

            return ParseSearchFilters(query, search.FTS);
        }

        public virtual IQueryable<TDb> AddSort(IQueryable<TDb> query, TSearch? search = null)
        {
            return query.OrderByDescending(m =>
                EF.Property<ICollection<object>>(m, "Ratings").Any()
                ? EF.Property<ICollection<object>>(m, "Ratings").Average(r => EF.Property<int>(r, "Value"))
                : 0);
        }


        public virtual IQueryable<TDb> ParseSearchFilters(IQueryable<TDb> query, string fts)
        {
            fts = fts.ToLower();

            var starAtLeastMatch = Regex.Match(fts, @"\bat\s*least\s*(\d+(\.\d+)?)\s*stars?\b", RegexOptions.IgnoreCase);
            var starExactMatch = Regex.Match(fts, @"^\s*(\d+(\.\d+)?)\s*stars?\s*$", RegexOptions.IgnoreCase);

            var afterYearMatch = Regex.Match(fts, @"\bafter\s+(\d{4})", RegexOptions.IgnoreCase);

            var olderThanYearsMatch = Regex.Match(fts, @"\bolder\s+than\s+(\d{1,4})\s*years?\b", RegexOptions.IgnoreCase);



            if (starExactMatch.Success)
            {
                int stars = int.Parse(starExactMatch.Groups[1].Value);
                if (typeof(TDb) == typeof(Database.Movie))
                {
                    query = query.Where(m =>
                        EF.Property<ICollection<Database.MovieRates>>(m, "Ratings").Any() &&
                        EF.Property<ICollection<Database.MovieRates>>(m, "Ratings").Average(r => EF.Property<int>(r, "Value")) == stars);
                }
                else if (typeof(TDb) == typeof(Database.TvShow))
                {
                    query = query.Where(t =>
                        EF.Property<ICollection<Database.TvShowRating>>(t, "Ratings").Any() &&
                        EF.Property<ICollection<Database.TvShowRating>>(t, "Ratings").Average(r => EF.Property<int>(r, "Value")) == stars);
                }
            }
            else if (starAtLeastMatch.Success)
            {
                double stars = double.Parse(starAtLeastMatch.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture);

                if (typeof(TDb) == typeof(Database.Movie))
                {
                    var queryMovies = query.Cast<Database.Movie>();

                    queryMovies = queryMovies.Where(m =>
                        m.Ratings.Any() &&
                        m.Ratings.Average(r => (double)r.Value) >= stars);

                    return queryMovies.Cast<TDb>();
                }

                else if (typeof(TDb) == typeof(Database.TvShow))
                {
                    var queryShows = query.Cast<Database.TvShow>();

                    queryShows = queryShows.Where(t =>
                        t.Ratings.Any() &&
                        t.Ratings.Average(r => (double)r.Value) >= stars);

                    return queryShows.Cast<TDb>();
                }

            }



            else if (afterYearMatch.Success)
            {
                int year = int.Parse(afterYearMatch.Groups[1].Value);
                query = query.Where(m => EF.Property<DateTime>(m, "ReleaseDate").Year > year);
            }
            else if (olderThanYearsMatch.Success)
            {
                int years = int.Parse(olderThanYearsMatch.Groups[1].Value);
                var cutoff = DateTime.Now.AddYears(-years);
                query = query.Where(m => EF.Property<DateTime>(m, "ReleaseDate") < cutoff);
            }
            else
            {
                if (typeof(TDb) == typeof(Database.Movie))
                {
                    var queryMovies = query.Cast<Database.Movie>();

                    queryMovies = queryMovies.Where(m =>
                        EF.Functions.Like(m.Title.ToLower(), $"%{fts}%") ||
                        EF.Functions.Like(m.Description.ToLower(), $"%{fts}%") ||
                        m.MovieActors.Any(ma => ma.Actor != null && ma.Actor.Name.ToLower().Contains(fts))
                    );

                    return queryMovies.Cast<TDb>();
                }
                else if (typeof(TDb) == typeof(Database.TvShow))
                {
                    var queryTvShows = query.Cast<Database.TvShow>();

                    queryTvShows = queryTvShows.Where(t =>
                        EF.Functions.Like(t.Title.ToLower(), $"%{fts}%") ||
                        EF.Functions.Like(t.Description.ToLower(), $"%{fts}%") ||
                        t.TvShowActors.Any(ta => ta.Actor != null && ta.Actor.Name.ToLower().Contains(fts))
                    );

                    return queryTvShows.Cast<TDb>();
                }
                else
                {
                    query = query.Where(m =>
                        EF.Functions.Like(EF.Property<string>(m, "Title").ToLower(), $"%{fts}%") ||
                        EF.Functions.Like(EF.Property<string>(m, "Description").ToLower(), $"%{fts}%")
                    );
                }
            }

            return query;
        }



        public virtual async Task<T> GetById(int id)
        {
            var entity = await _context.Set<TDb>().FindAsync(id);
            return _mapper.Map<T>(entity);
        }

        public virtual async Task AddRatingAsync(int id, int value)
        {
            if (value < 1 || value > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            var entity = await _context.Set<TDb>().FindAsync(id);
            if (entity == null)
                throw new Exception($"{typeof(TDb).Name} not found.");

            if (typeof(TDb) == typeof(Database.Movie))
            {
                var rating = new Database.MovieRates
                {
                    MovieId = id,
                    Value = value
                };
                _context.Set<Database.MovieRates>().Add(rating);
            }
            else if (typeof(TDb) == typeof(Database.TvShow))
            {
                var rating = new Database.TvShowRating
                {
                    TvShowId = id,
                    Value = value
                };
                _context.Set<Database.TvShowRating>().Add(rating);
            }
            else
            {
                throw new NotSupportedException("Rating not supported for this entity.");
            }

            await _context.SaveChangesAsync();
        }
    }
}
