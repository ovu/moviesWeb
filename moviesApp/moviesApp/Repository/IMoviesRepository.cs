using System.Collections.Generic;
using moviesApp.Model;

namespace moviesApp.Controllers
{
    public interface IMoviesRepository
    {
        IEnumerable<Movie> ListMovies();
    }
}