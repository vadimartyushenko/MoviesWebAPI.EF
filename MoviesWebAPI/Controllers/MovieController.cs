using Microsoft.AspNetCore.Mvc;
using MoviesWebAPI.EF.Models;
using System.Data;
using System.Data.SqlClient;

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

            var moviesToView = movies.Select(x => new MovieView(x.Title, x.Genre.Name, string.Join("\r\n", x.MovieActors.Select(act => act.Actor.Name))));
            return Json(moviesToView);
        }

        [HttpPost]
        public JsonResult Post(Movie movie)
        {
            string query = @"
                            insert 
                            ";
            var dt = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("LocalMoviedb");
            SqlDataReader reader;
            using (var con = new SqlConnection(sqlDataSource)) {
                con.Open();
                using var command = new SqlCommand(query, con);
                reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                con.Close();
            }
            return new JsonResult(dt);
        }
    }
}
