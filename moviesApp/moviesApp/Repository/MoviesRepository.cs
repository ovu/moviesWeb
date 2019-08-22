using System.Collections.Generic;
using moviesApp.Controllers;
using moviesApp.Model;

namespace moviesApp.Repository
{
    public class MoviesRepository : IMoviesRepository
    {

        public IEnumerable<Movie> ListMovies()
        {
            Movie movie = new Movie { Title = "Test", Actors = "the Actors", Director = "Best director", Image = "anImage", Year = 2000 };

            List<Movie> movies = new List<Movie>();
            movies.Add(movie);
            return movies;

        }
    }
}
