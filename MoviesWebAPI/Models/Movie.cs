using System;
using System.Collections.Generic;

namespace MoviesWebAPI.EF.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieActors = new HashSet<MovieActor>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }
}
