using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.DataAccess.Entities.Identity
{
    public class ApplicationUser : MongoUser
    {
        
        public override string Id { get; set; }
    }
}