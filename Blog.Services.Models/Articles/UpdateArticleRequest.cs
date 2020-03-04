using System.ComponentModel.DataAnnotations;

namespace Blog.Services.Articles
{
    public class UpdateArticleRequest
    {
        [Required]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Title { get; set; }

        [StringLength(2000, ErrorMessage = "Article length can't be more than 2000 symbols.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}