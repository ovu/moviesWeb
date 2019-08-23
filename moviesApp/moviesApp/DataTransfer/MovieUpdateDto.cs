using moviesApp.Model;

namespace moviesApp.DataTransfer
{
    public class MovieUpdateDto: MovieDto
    {
        public string Id { get; set; }

        public override Movie ToMovie()
        {
            var movie = new Movie(Title, Director, Actors, Image, Year)
            {
                MovieId = Id
            };

            return movie;
        }
    }
}
