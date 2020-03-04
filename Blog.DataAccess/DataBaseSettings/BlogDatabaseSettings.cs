namespace Blog.DataAccess.DataBaseSettings
{
    public class BlogDatabaseSettings : IBlogDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string UserCollectionName { get; set; }

        public string ArticleCollectionName { get; set; }

        public string CommentsCollectionName { get; set; }
    }
}