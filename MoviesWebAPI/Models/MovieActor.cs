using System;
using System.Collections.Generic;

namespace MoviesWebAPI.EF.Models
{
    public partial class MovieActor
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public int? ActorId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
