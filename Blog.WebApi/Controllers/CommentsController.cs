using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Services.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService ?? throw new ArgumentNullException(nameof(commentsService));
        }

        // GET: api/Comments
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentsService.GetCommentsAsync();
            return Ok(comments);
        }

        // GET: api/Comments/5
        [HttpGet("{id:length(24)}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComment(string id)
        {
            var comment = await _commentsService.GetCommentAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST: api/Comments
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] UpdateCommentRequest updateComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = HttpContext.User.Claims.First().Value;
            updateComment.UserId = userId;

            Comment comment = await _commentsService.AddCommentAsync(updateComment);
            string location = $"api/[controller]/{comment.Id}";

            return Created(location, comment);
        }

        // PUT: api/Comments/5
        [HttpPut("{id:length(24)}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCommentRequest updateComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.Claims.First().Value;
            updateComment.UserId = userId;

            await _commentsService.UpdateCommentAsync(id, updateComment);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:length(24)}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await _commentsService.DeleteCommentAsync(id);

            return NoContent();
        }
    }
}