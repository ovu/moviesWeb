using moviesApp.DataTransfer;

namespace moviesApp.Model
{
    public class MovieInsertDto: MovieDto
    {
        public override Movie ToMovie()
        {
            return new Movie(Title, Director, Actors, Image, Year);
        }
    }
}
