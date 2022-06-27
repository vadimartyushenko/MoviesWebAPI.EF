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
            /*string query = @"
                            select m.Id, m.Title, g.Name from Movie m
                            join Genre g on g.Id = m.GenreId
                            ";*/
            var query = @"
SELECT m.Id, m.Title, g.Name,
  STUFF
  (
    (SELECT ',' + a.Name
     FROM MovieActors AS ma
	 join Actor a on ma.ActorId = a.Id
     WHERE ma.MovieId = m.Id
	 order by a.Id desc
     FOR XML PATH('')), 1, 1, NULL
  ) AS Actors
FROM Movie m
join Genre g on g.Id = m.GenreId
";
            var dt = new DataTable();
            var data = _dbContext.Movies.ToArray();
            var sqlDataSource = _configuration.GetConnectionString("LocalMoviedb");
            SqlDataReader reader;
            using(var con = new SqlConnection(sqlDataSource)) {
                con.Open();
                using var command = new SqlCommand(query, con);
                reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                con.Close();
            }
            return new JsonResult(dt);
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
