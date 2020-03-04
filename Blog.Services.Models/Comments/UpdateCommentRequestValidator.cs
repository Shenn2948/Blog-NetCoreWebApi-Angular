using FluentValidation;

namespace Blog.Services.Comments
{
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidator()
        {
            RuleFor(x => x.Content).MaximumLength(200);
            RuleFor(x => x.ArticleId).NotEmpty().MaximumLength(24);
        }
    }
}