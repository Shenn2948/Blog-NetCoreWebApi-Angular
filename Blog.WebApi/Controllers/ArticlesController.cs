using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Services.Articles;
using Blog.Services.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ICommentsService _commentsService;

        public ArticlesController(IArticleService articleService, ICommentsService commentsService)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            _commentsService = commentsService ?? throw new ArgumentNullException(nameof(commentsService));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticles()
        {
            IEnumerable<Article> articles = await _articleService.GetArticlesAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticle(string id)
        {
            var article = await _articleService.GetArticleAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpGet("{id}/comments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentsOfArticleAsync(string id)
        {
            var comments = await _commentsService.GetCommentsOfArticleAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] UpdateArticleRequest updateArticleIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.Claims.First().Value;
            updateArticleIn.UserId = userId;

            var article = await _articleService.AddArticleAsync(updateArticleIn);
            var location = $"api/[controller]/{article.Id}";

            return Created(location, article);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateArticleRequest updateArticleIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.Claims.First().Value;
            updateArticleIn.UserId = userId;

            await _articleService.UpdateArticleAsync(id, updateArticleIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await _articleService.DeleteArticleAsync(id);

            return NoContent();
        }
    }
}