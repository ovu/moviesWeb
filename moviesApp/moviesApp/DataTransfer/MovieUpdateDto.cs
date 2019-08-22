using moviesApp.Model;

namespace moviesApp.DataTransfer
{
    public class MovieUpdateDto: MovieDto
    {
        string Id { get; set; }

        public override Movie ToMovie()
        {
            var movie = new Movie(Title, Director, Actors, Image, Year);
            movie.MovieId = Id;
            return movie;
        }
    }
}
