using System;


namespace Blog.Services.Comments
{
    public class Comment
    {
        /// <summary>
        /// Gets or sets a comment ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the created date.</summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>Gets or sets the article identifier.</summary>
        /// <value>The article identifier.</value>
        public string ArticleId { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }
    }
}