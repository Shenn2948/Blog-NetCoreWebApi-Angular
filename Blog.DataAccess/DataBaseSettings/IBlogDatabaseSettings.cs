namespace Blog.DataAccess.DataBaseSettings
{
    public interface IBlogDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string UserCollectionName { get; set; }

        string ArticleCollectionName { get; set; }

        string CommentsCollectionName { get; set; }
    }
}