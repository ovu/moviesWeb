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

        [HttpGet]
        public async Task<IEnumerable<MovieResponseDto>> MoviesList()
        {
            var movies = await moviesRepository.ListMovies();
            return movies.ToListResponseMovie();
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponseDto>> SearchMovies([FromQuery]string textToSearch)
        {
            var movies = await moviesRepository.FindMovies(textToSearch);
            return movies.ToListResponseMovie();
        }

        [HttpPost]
        public async Task<MovieResponseDto> InsertMovie([FromBody]MovieRequestDto movieRequestDto)
        {
            Movie savedMovie = await moviesRepository.InsertMovie(movieRequestDto.ToMovie());
            return savedMovie.ToResponseMovie();
        }

        [HttpPut("{movieId}")]
        public async Task<bool> UpdateMovie([FromRoute]string movieId,[FromBody]MovieRequestDto movieRequestDto)
        {
            return await moviesRepository.UpdateMovie(movieRequestDto.ToMovie(movieId));
        }

        [HttpDelete("{movieId}")]
        public async Task<bool> DeleteMovie([FromRoute]string movieId)
        {
            return await moviesRepository.DeleteMovie(movieId);
        }

    }
}
