using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using moviesApp.DataTransfer;
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
        public async Task<IEnumerable<Movie>> MoviesList()
        {
            return await moviesRepository.ListMovies();
        }

        [HttpPost("[action]")]
        public async Task<Movie> InsertMovie([FromBody]MovieInsertDto movieInsertDto)
        {
            return await moviesRepository.InsertMovie(movieInsertDto.ToMovie());
        }

        [HttpPut("[action]")]
        public async Task<bool> UpdateMovie([FromBody]MovieUpdateDto movieUpdateDto)
        {
            return await moviesRepository.UpdateMovie(movieUpdateDto.ToMovie());
        }
    }
}
