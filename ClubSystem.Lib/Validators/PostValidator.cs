using ClubSystem.Lib.Models.Entities;
using FluentValidation;

namespace ClubSystem.Lib.Validators
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.UserPosts).NotEmpty();
        }
    }
}
