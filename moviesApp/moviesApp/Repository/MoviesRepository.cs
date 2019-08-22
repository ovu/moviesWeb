using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using moviesApp.Controllers;
using moviesApp.Model;

namespace moviesApp.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        public MoviesRepository()
        {
            // or use a connection string
            client = new MongoClient("mongodb+srv://service:rSSCDZTIZzwPRsnEHGvY@cluster0-iv5oy.mongodb.net");
            database = client.GetDatabase("movie");

            collection = database.GetCollection<BsonDocument>("movie");
        }

        public async Task<IEnumerable<Movie>> ListMovies()
        {
            var documents = await collection.Find(new BsonDocument()).ToListAsync();
            List<Movie> movies = new List<Movie>();

            foreach (BsonDocument doc in documents)
            {

                Movie movie = new Movie
                {
                    Title = doc.GetString("title"),
                    Actors = doc.GetString("actors"),
                    Director = doc.GetString("director"),
                    Image = doc.GetString("image"),
                    Year = doc.GetInt("year")
                };

                movies.Add(movie);
            }


            return movies;

        }
    }
}
