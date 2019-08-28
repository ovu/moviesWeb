using MongoDB.Bson;

namespace moviesApp.Controllers.Validation
{
    public static class ExtensionMethods
    {
        public static bool isValidObjectId(this string objectId, out ObjectId resultObjectId)
        {
            if (ObjectId.TryParse(objectId, out resultObjectId))
            {
                if (resultObjectId.ToString() == objectId)
                {
                    return true;
                }
            }
            return false;
            
        }
    }
}
