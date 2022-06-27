namespace MoviesWebAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public GenreType Genre { get; set; }
        public List<Actor> Actors { get; set; }
    }

    public enum GenreType
    {
        Drama,
        Comedy,
        Horror,
    }
}
