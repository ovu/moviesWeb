using System.Collections.Generic;
using System.Threading.Tasks;
using moviesApp.Model;

namespace moviesApp.Controllers
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> ListMovies();
        Task<IEnumerable<Movie>> FindMovies(string textToSearch);
        Task<Movie> InsertMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(string movieId);
        Task<Movie> FindMovie(string movieId);
    }
}