using Microsoft.AspNetCore.Mvc;
using MoviesWebAPI.EF.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace MoviesWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MovieDbContext _dbContext;
        private readonly ILogger<MovieController> _logger;
        public MovieController(IConfiguration configuration, MovieDbContext dbContext, ILogger<MovieController> logger)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var movies = _dbContext.Movies.ToArray();
            if (movies.Length == 0)
                return Json(null);

            var actors = _dbContext.Actors.ToArray();
            var genres = _dbContext.Genres.ToArray();
            var movieActors = _dbContext.MovieActors.ToArray();

            var moviesToView = movies.Select(x => new MovieViewModel(x.Id, x.Title, x.Genre.Name, string.Join("; ", x.MovieActors.Select(act => act.Actor.Name))));
            return Json(moviesToView);
        }

        [HttpPost]
        public JsonResult Post(MovieViewModel movie)
        {
            if (!movie.IsValidTitle())
                return new JsonResult("Not valid title!") {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            var movies = _dbContext.Movies.ToArray();
            if (movies.Select(x => x.Title).Contains(movie.Title))
                return new JsonResult("Movie with this title exist in DB!") {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

            var genres = _dbContext.Genres.ToArray();
            if (!movie.IsValidGenre(genres.Select(g => g.Name))) 
                return new JsonResult($"Not found genre \'{movie.Genre}\'!") {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

            var actors = _dbContext.Actors.ToArray();
            var newActors = new List<Actor>();
            var actorsFromRequest = movie.Actors.Split(';');
            foreach (var actor in actorsFromRequest)
                if (!IsExistActor(actor, actors.Select(a => a.Name)))
                    newActors.Add(new Actor()
                    {
                        Name = actor
                    });

            if (newActors.Any()) {
                _dbContext.Actors.AddRange(newActors); 
                _dbContext.SaveChanges();
                actors = _dbContext.Actors.ToArray();
            }

            var genre = genres.Single(x => x.Name == movie.Genre);
            var newMovie = new Movie() {
                Title = movie.Title,
                Genre = genre,
                GenreId = genre.Id,

            };
            var links = new List<MovieActor>(actorsFromRequest.Length);
            foreach (var act in actorsFromRequest) 
                links.Add(new MovieActor()
                {
                    Actor = actors.Single(a => a.Name == act),
                    Movie = newMovie

                });

            newMovie.MovieActors = links;
            _dbContext.Movies.Add(newMovie);
            _dbContext.MovieActors.AddRange(links);
            _dbContext.SaveChanges();
            return new JsonResult("Added Successfully");
        }

        #region Helped Methods

        private static bool IsExistActor(string actorName, IEnumerable<string> existActorNames)
        {
            return existActorNames.Contains(actorName.Trim());
        }

        #endregion
    }
}
