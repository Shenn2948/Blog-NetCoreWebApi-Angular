using System.Collections.Generic;
using System.Threading.Tasks;

using Blog.DataAccess.Entities;

namespace Blog.Services.Comments
{
    public interface ICommentsService
    {
        Task<Comment> GetCommentAsync(string commentId);

        Task<IEnumerable<Comment>> GetCommentsAsync();

        Task<IEnumerable<Comment>> GetCommentsOfUserAsync(string userId);

        Task<IEnumerable<Comment>> GetCommentsOfArticleAsync(string articleId);

        Task<Comment> AddCommentAsync(UpdateCommentRequest updateCommentIn);

        Task<Comment> UpdateCommentAsync(string id, UpdateCommentRequest updateCommentIn);

        Task DeleteCommentAsync(string commentId);
    }
}