namespace Blog.Services.Users
{
    public class User
    {
        /// <summary>
        /// Gets or sets a user ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}