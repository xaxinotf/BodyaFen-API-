using BodyaFen_API_.Contexts;
using BodyaFen_API_.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BodyaFen_API_.Dopomoga
{
    public class GenreService : Interface  
    {
        private readonly BodyaFenDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public string cacheKey = "genre";

        public GenreService(IMemoryCache memoryCache, BodyaFenDbContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            if (!_memoryCache.TryGetValue(cacheKey, out genres))
            {
                genres = GetValuesFromDb(); 
                _memoryCache.Set(cacheKey, genres,
                    new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(300)));
            }
            return genres;
        }

        public void AddGenre(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre != null)
            {
                var genres = GetGenres();
                if(genres != null && !genres.Contains(genre)) { 
                    genres.Add(genre);
                }
                _memoryCache.Set(cacheKey, genres);
            }
        }

        public void DeleteGenre(int genreId)
        {
            var genre = _context.Genres.Find(genreId);
            if (genre != null)
            {
                var genres = GetGenres();
                if (genres != null && !genres.Contains(genre))
                {
                    genres.Remove(genre);
                }
                _memoryCache.Set(cacheKey, genres);
            }
            
        }

        public void UpdateGenre(long id)
        {

            var genre = _context.Genres.Find(id);
            if (genre != null)
            {
                var genres = GetGenres();
                if (genres != null && !genres.Contains(genre))
                {
                    for (int i = 0; i< genres.Count; i++)
                    {
                        if (genres[i].Id == genre.Id)
                        {
                            genres[i] = genre;
                            break;
                        }
                    }
                }
                _memoryCache.Set(cacheKey, genres);
            }
        }
        private List<Genre> GetValuesFromDb()
        {
            var genres = _context.Genres.ToList();


            return genres; 
        }

        
    }
}
