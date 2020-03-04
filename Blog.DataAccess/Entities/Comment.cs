using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.DataAccess.Entities
{
    public class Comment
    {
        /// <summary>
        /// Gets or sets a comment ID.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>Gets or sets the created date.</summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        [BsonRepresentation(BsonType.ObjectId)]
        public string ArticleId { get; set; }

        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }
    }
}