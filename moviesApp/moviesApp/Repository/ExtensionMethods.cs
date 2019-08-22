using MongoDB.Bson;

namespace moviesApp.Repository
{
    public static class ExtensionMethods
    {
        public static string GetString(this BsonDocument document, string property)
        {
            BsonValue bsonValue;
            string result;
            if (document.TryGetValue(property, out bsonValue))
            {
                result = bsonValue.AsString;
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }

        public static int GetInt(this BsonDocument document, string property)
        {
            BsonValue bsonValue;
            int result;
            if (document.TryGetValue(property, out bsonValue))
            {
                if(bsonValue.IsInt32)
                {
                    result = bsonValue.AsInt32;
                } else
                {
                    result = 0;
                }
            }
            else
            {
                result = 0;
            }
            return result;
        }
    }
}
