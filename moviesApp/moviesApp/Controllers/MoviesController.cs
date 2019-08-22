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
            return moviesRepository.ListMovies();
        }
    }
}
