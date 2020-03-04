using System;
using System.Collections.Generic;
using Blog.Services.Comments;
using Blog.Services.Users;

namespace Blog.Services.Articles
{
    public class Article
    {
        /// <summary>
        /// Gets or sets a article ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>Gets or sets the created date.</summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public User User { get; set; }

        /// <summary>Gets or sets the comments.</summary>
        /// <value>The comments.</value>
        public IList<Comment> Comments { get; set; }
    }
}