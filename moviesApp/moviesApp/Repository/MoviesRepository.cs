using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using moviesApp.Controllers;
using moviesApp.Model;

namespace moviesApp.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Movie> collection;
        public MoviesRepository()
        {
            BsonClassMap.RegisterClassMap<Movie>(cm =>
            {
                cm.AutoMap();
                
                cm.MapMember(c => c.Title).SetElementName("title");
                cm.MapMember(c => c.Director).SetElementName("director");
                cm.MapMember(c => c.Image).SetElementName("image");
                cm.MapMember(c => c.Actors).SetElementName("actors");
                cm.MapMember(c => c.Year).SetElementName("year");
                
            });

            // Use the connection string
            client = new MongoClient("mongodb+srv://service:rSSCDZTIZzwPRsnEHGvY@cluster0-iv5oy.mongodb.net");
            database = client.GetDatabase("movie");

            collection = database.GetCollection<Movie>("movie");
        }

        public async Task<IEnumerable<Movie>> ListMovies()
        {
            var documents = await collection.AsQueryable().ToListAsync();
            var movies = new List<Movie>();

            foreach (Movie movie in documents)
            {
                movies.Add(movie);
            }

            return movies;

        }

        public async Task<Movie> InsertMovie(Movie movie)
        {
            await collection.InsertOneAsync(movie);

            return movie;
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            var result = await collection.ReplaceOneAsync (x => x.Id == movie.Id, movie);

            if(result.IsModifiedCountAvailable && result.ModifiedCount == 1)
            {
                return true;
            }

            return false;
        }
    }
}
