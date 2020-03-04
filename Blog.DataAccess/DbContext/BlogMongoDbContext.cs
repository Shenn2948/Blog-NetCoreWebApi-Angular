using Blog.DataAccess.DataBaseSettings;
using Blog.DataAccess.Entities;
using Blog.DataAccess.Entities.Identity;

using MongoDB.Driver;

namespace Blog.DataAccess.DbContext
{
    public class BlogMongoDbContext : IMongoDbContext
    {
        public BlogMongoDbContext(IBlogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            IMongoDatabase mongoDb = client.GetDatabase(settings.DatabaseName);

            Users = mongoDb.GetCollection<ApplicationUser>(settings.UserCollectionName);
            Articles = mongoDb.GetCollection<Article>(settings.ArticleCollectionName);
            Comments = mongoDb.GetCollection<Comment>(settings.CommentsCollectionName);
        }

        public IMongoCollection<ApplicationUser> Users { get; }

        public IMongoCollection<Article> Articles { get; }

        public IMongoCollection<Comment> Comments { get; }
    }
}