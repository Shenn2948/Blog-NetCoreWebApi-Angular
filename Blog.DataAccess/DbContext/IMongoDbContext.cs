using Blog.DataAccess.Entities;
using Blog.DataAccess.Entities.Identity;

using MongoDB.Driver;

namespace Blog.DataAccess.DbContext
{
    public interface IMongoDbContext
    {
        IMongoCollection<ApplicationUser> Users { get; }

        IMongoCollection<Article> Articles { get; }

        IMongoCollection<Comment> Comments { get; }
    }
}