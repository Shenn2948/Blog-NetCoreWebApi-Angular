using System.ComponentModel.DataAnnotations;

namespace Blog.Services.Comments
{
    public class UpdateCommentRequest
    {
        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        [StringLength(200, ErrorMessage = "Comment length can't be more than 200 symbols.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        [Required]
        public string ArticleId { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }
    }
}