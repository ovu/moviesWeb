using System.Collections.Generic;
using System.Linq;
using moviesApp.DataTransfer;

namespace moviesApp.Model
{
    public static class ExtensionMethods
    {
        public static MovieResponseDto ToResponseMovie(this Movie movie)
        {
            return new MovieResponseDto
            {
                Id = movie.Id.ToString(),
                Title = movie.Title,
                Director = movie.Director,
                Actors = movie.Actors,
                Image = movie.Image,
                Year = movie.Year
            };
        }

        public static IEnumerable<MovieResponseDto> ToListResponseMovie(this IEnumerable<Movie> movieList)
        {
            var result = movieList.Select(movie => new MovieResponseDto
            {
                Id = movie.Id.ToString(),
                Title = movie.Title,
                Director = movie.Director,
                Actors = movie.Actors,
                Image = movie.Image,
                Year = movie.Year
            });

            return result;
        }
    }
}
