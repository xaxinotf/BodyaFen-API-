using BodyaFen_API_.Models;

namespace BodyaFen_API_.Dopomoga
{
    public interface Interface
    {
        public List<Genre> GetGenres();
        public void AddGenre(int id);

        public void DeleteGenre(int genreId);

        public void UpdateGenre(long id);

    }
}
