using System.Runtime.CompilerServices;

namespace MoviesWebAPI.EF.Models
{
    public record MovieViewModel(int MovieId, string Title, string Genre, string Actors);

    public static class Extensions
    {
        public static bool IsValidGenre(this MovieViewModel movie, IEnumerable<string> existGenreNames) => existGenreNames.Contains(movie.Genre);

        public static bool IsValidTitle(this MovieViewModel movie) => !string.IsNullOrWhiteSpace(movie.Title);
    }
    
}
