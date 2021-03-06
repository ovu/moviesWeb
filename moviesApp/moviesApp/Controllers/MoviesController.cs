﻿using System.Collections.Generic;
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
        public async Task<IActionResult> InsertMovie([FromBody]MovieRequestDto movieRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Movie savedMovie = await moviesRepository.InsertMovie(movieRequestDto.ToMovie());
            return Ok(savedMovie.ToResponseMovie());
        }

        [HttpPut("{movieId}")]
        public async Task<IActionResult> UpdateMovie([FromRoute]string movieId,[FromBody]MovieRequestDto movieRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var objectId = new ObjectId();
            if (movieId.isValidObjectId(out objectId))
            {
                var updateResult = await moviesRepository.UpdateMovie(movieRequestDto.ToMovie(objectId.ToString()));
                return Ok(updateResult);
            }

            return BadRequest();
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovie([FromRoute]string movieId)
        {
            var objectId = new ObjectId();
            if (movieId.isValidObjectId(out objectId))
            {
                var result = await moviesRepository.FindMovie(objectId);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovie([FromRoute]string movieId)
        {
            var objectId = new ObjectId();
            if (movieId.isValidObjectId(out objectId))
            {
                var result = await moviesRepository.DeleteMovie(objectId);
                return Ok(result);
            }

            return BadRequest();
        }

    }
}
