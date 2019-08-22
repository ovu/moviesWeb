using System.Collections.Generic;
using System.Threading.Tasks;
using moviesApp.Model;

namespace moviesApp.Controllers
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> ListMovies();
    }
}