using System.Collections.Generic;
using System.Threading.Tasks;

using Blog.DataAccess.Entities;

namespace Blog.Services.Articles
{
    public interface IArticleService
    {
        Task<Article> GetArticleAsync(string articleId);

        Task<IEnumerable<Article>> GetArticlesAsync();

        Task<IEnumerable<Article>> GetArticlesOfUserAsync(string userId);

        Task<Article> AddArticleAsync(UpdateArticleRequest updateArticleIn);

        Task<Article> UpdateArticleAsync(string id, UpdateArticleRequest updateArticleIn);

        Task DeleteArticleAsync(string articleId);
    }
}