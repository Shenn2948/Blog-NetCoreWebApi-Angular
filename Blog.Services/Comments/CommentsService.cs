using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Blog.DataAccess.DbContext;
using Blog.DataAccess.Entities;
using Blog.DataAccess.Entities.Identity;
using Blog.Services.Exceptions;

using MongoDB.Driver;

using DataAccessComment = Blog.DataAccess.Entities.Comment;

namespace Blog.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IMongoDbContext _dbContext;

        private readonly IMapper _mapper;

        public CommentsService(IMongoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Comment> GetCommentAsync(string commentId)
        {
            DataAccessComment dbComment = await _dbContext.Comments.Find(x => x.Id == commentId).FirstOrDefaultAsync();

            return _mapper.Map<Comment>(dbComment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            List<DataAccessComment> dbComments = await _dbContext.Comments.Find(_ => true).ToListAsync();
            IEnumerable<Comment> comments = dbComments.Select(_mapper.Map<Comment>);

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetCommentsOfUserAsync(string userId)
        {
            List<DataAccessComment> dbComments = await _dbContext.Comments.Find(x => x.UserId == userId).ToListAsync();
            IEnumerable<Comment> comments = dbComments.Select(_mapper.Map<Comment>);

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetCommentsOfArticleAsync(string articleId)
        {
            List<DataAccessComment> dbComments = await _dbContext.Comments.Find(x => x.ArticleId == articleId).ToListAsync();
            IEnumerable<Comment> comments = dbComments.Select(_mapper.Map<Comment>);

            return comments;
        }

        public async Task<Comment> AddCommentAsync(UpdateCommentRequest updateCommentIn)
        {
            ApplicationUser dbUser = await _dbContext.Users.Find(x => x.Id == updateCommentIn.UserId).FirstOrDefaultAsync();
            Article dbArticle = await _dbContext.Articles.Find(x => x.Id == updateCommentIn.ArticleId).FirstOrDefaultAsync();

            if (dbUser == null || dbArticle == null)
            {
                string message = dbUser == null ? "user" : "article";
                throw new RequestedResourceNotFoundException($"{message} with this id wasn't found.");
            }

            DataAccessComment dbComment = _mapper.Map<UpdateCommentRequest, DataAccessComment>(updateCommentIn);

            await _dbContext.Comments.InsertOneAsync(dbComment);

            return _mapper.Map<Comment>(dbComment);
        }

        public async Task<Comment> UpdateCommentAsync(string id, UpdateCommentRequest updateCommentIn)
        {
            var filter = Builders<DataAccessComment>.Filter.Eq(s => s.Id, id);
            var update = Builders<DataAccessComment>.Update.Set(s => s.Content, updateCommentIn.Content);

            DataAccessComment dbArticle = await _dbContext.Comments.FindOneAndUpdateAsync(filter, update);

            return _mapper.Map<Comment>(dbArticle);
        }

        public async Task DeleteCommentAsync(string commentId)
        {
            var dbComment = await _dbContext.Comments.Find(x => x.Id == commentId).FirstOrDefaultAsync();

            if (dbComment == null)
            {
                throw new RequestedResourceNotFoundException($"comment with{commentId} wasn't found.");
            }

            await _dbContext.Comments.DeleteOneAsync(Builders<DataAccessComment>.Filter.Eq(x => x.Id, commentId));
        }
    }
}