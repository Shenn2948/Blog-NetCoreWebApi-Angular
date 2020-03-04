using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.DataAccess.DbContext;
using Blog.DataAccess.Entities;
using Blog.Services.Exceptions;
using MongoDB.Driver;
using DataAccessArticle = Blog.DataAccess.Entities.Article;

namespace Blog.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IMongoDbContext _dbContext;

        private readonly IMapper _mapper;

        public ArticleService(IMongoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Article> GetArticleAsync(string articleId)
        {
            var dbArticle = await _dbContext.Articles.Find(x => x.Id == articleId).FirstOrDefaultAsync();

            if (dbArticle == null)
            {
                throw new RequestedResourceNotFoundException($"article with{articleId} wasn't found.");
            }

            return _mapper.Map<Article>(dbArticle);
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            List<DataAccessArticle> dbArticles = await _dbContext.Articles.Find(_ => true).SortBy(x => x.Title).ToListAsync();
            IEnumerable<Article> articles = dbArticles.Select(_mapper.Map<Article>);

            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticlesOfUserAsync(string userId)
        {
            var dbArticles = await _dbContext.Articles.Find(x => x.User.Id == userId).SortBy(x => x.Title).ToListAsync();
            var articles = dbArticles.Select(_mapper.Map<Article>);

            return articles;
        }

        public async Task<Article> AddArticleAsync(UpdateArticleRequest updateArticleIn)
        {
            var dbArticles = await _dbContext.Articles.FindAsync(article => article.Title == updateArticleIn.Title);

            if (dbArticles.Any())
            {
                throw new RequestedResourceHasConflictException();
            }

            var dbUser = await _dbContext.Users.Find(x => x.Id == updateArticleIn.UserId).FirstOrDefaultAsync();

            if (dbUser == null)
            {
                throw new RequestedResourceNotFoundException();
            }

            DataAccessArticle dbArticle = _mapper.Map<UpdateArticleRequest, DataAccessArticle>(updateArticleIn);
            dbArticle.User = dbUser;

            await _dbContext.Articles.InsertOneAsync(dbArticle);

            return _mapper.Map<Article>(dbArticle);
        }

        public async Task<Article> UpdateArticleAsync(string articleId, UpdateArticleRequest updateArticleIn)
        {
            var filter = Builders<DataAccessArticle>.Filter.Eq(s => s.Id, articleId);
            var update = Builders<DataAccessArticle>.Update.Set(s => s.Title, updateArticleIn.Title).Set(s => s.Content, updateArticleIn.Content);

            DataAccessArticle dbArticle = await _dbContext.Articles.FindOneAndUpdateAsync(filter, update);

            return _mapper.Map<Article>(dbArticle);
        }

        public async Task DeleteArticleAsync(string articleId)
        {
            var dbArticle = await _dbContext.Articles.Find(x => x.Id == articleId).FirstOrDefaultAsync();

            if (dbArticle == null)
            {
                throw new RequestedResourceNotFoundException($"article with{articleId} wasn't found.");
            }

            await _dbContext.Articles.DeleteOneAsync(Builders<DataAccessArticle>.Filter.Eq(x => x.Id, articleId));
        }
    }
}