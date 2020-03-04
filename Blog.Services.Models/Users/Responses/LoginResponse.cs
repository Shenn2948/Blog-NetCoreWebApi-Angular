namespace Blog.Services.Users.Responses
{
    public class LoginResponse
    {
        public LoginResponse(string token, string userName, string email, string id)
        {
            Token = token;
            UserName = userName;
            Email = email;
            Id = id;
        }

        public string Id { get; }

        public string Token { get; }

        public string UserName { get; }

        public string Email { get; }
    }
}