using System;

using Blog.DataAccess.Entities.Identity;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.DataAccess.Entities
{
    public class Article
    {
        /// <summary>
        /// Gets or sets a article ID.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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
        public ApplicationUser User { get; set; }
    }
}