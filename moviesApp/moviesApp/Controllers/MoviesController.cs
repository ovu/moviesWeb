using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using moviesApp.Controllers.Validation;
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

        [HttpGet("list")]
        public async Task<IEnumerable<MovieResponseDto>> MoviesList()
        {
            var movies = await moviesRepository.ListMovies();
            return movies.ToListResponseMovie();
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponseDto>> SearchMovies([FromQuery]string textToSearch)
        {
            IEnumerable<MovieResponseDto> result = new List<MovieResponseDto>();
            if (textToSearch != string.Empty)
            {
                var movies = await moviesRepository.FindMovies(textToSearch);
                result = movies.ToListResponseMovie();
            }

            return result;
        }

        [HttpPost]
        public async Task<MovieResponseDto> InsertMovie([FromBody]MovieRequestDto movieRequestDto)
        {
            Movie savedMovie = await moviesRepository.InsertMovie(movieRequestDto.ToMovie());
            return savedMovie.ToResponseMovie();
        }

        [HttpPut("{movieId}")]
        public async Task<IActionResult> UpdateMovie([FromRoute]string movieId,[FromBody]MovieRequestDto movieRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var objectId = new ObjectId();
            bool updateResult = false;
            if (movieId.isValidObjectId(out objectId))
            {
                updateResult = await moviesRepository.UpdateMovie(movieRequestDto.ToMovie(objectId.ToString()));
                return Ok(updateResult);
            }

            return BadRequest();
        }

        [HttpGet("{movieId}")]
        public async Task<MovieResponseDto> GetMovie([FromRoute]string movieId)
        {
            Movie movie = await moviesRepository.FindMovie(movieId);
            return movie.ToResponseMovie();
        }

        [HttpDelete("{movieId}")]
        public async Task<bool> DeleteMovie([FromRoute]string movieId)
        {
            return await moviesRepository.DeleteMovie(movieId);
        }

    }
}
