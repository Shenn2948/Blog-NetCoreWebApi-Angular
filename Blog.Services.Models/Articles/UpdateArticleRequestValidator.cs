using FluentValidation;

namespace Blog.Services.Articles
{
    public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
    {
        public UpdateArticleRequestValidator()
        {
            RuleFor(x => x.Title).Length(4, 60);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(2000);
        }
    }
}