using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using moviesApp.Model;

namespace moviesApp.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Movie> MoviesList()
        {
            Movie movie = new Movie { Title = "Test", Actors = "the Actors", Director = "Best director", Image = "anImage", Year = 2000 };

            List<Movie> movies = new List<Movie>();
            movies.Add(movie);
            return movies;

        }
    }
}
