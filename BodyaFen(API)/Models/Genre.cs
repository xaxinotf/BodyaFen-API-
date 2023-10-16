namespace BodyaFen_API_.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Song>? Songs { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Genre otherGenre)
            {
                return Id == otherGenre.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
