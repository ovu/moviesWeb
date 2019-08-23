using MongoDB.Bson;
using moviesApp.Model;

namespace moviesApp.DataTransfer
{
    public class MovieRequestDto: MovieDto
    {
        public Movie ToMovie()
        {
            return new Movie(Title, Director, Actors, Image, Year);
        }

        public Movie ToMovie(string movieId)
        {
            var movie = new Movie(Title, Director, Actors, Image, Year)
            {
                Id = new ObjectId(movieId)
            };

            return movie;
        }
    }
}
