namespace BodyaFen_API_.Models
{
    public class SongCreationDto
    {
        public string Name { get; set; }
        public Guid ArtistId { get; set; }
        public int GenreId { get; set; }
    }
}
