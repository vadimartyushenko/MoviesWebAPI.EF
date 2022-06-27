using System;
using System.Collections.Generic;

namespace MoviesWebAPI.EF.Models
{
    public partial class Actor
    {
        public Actor()
        {
            MovieActors = new HashSet<MovieActor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }
    }
}
