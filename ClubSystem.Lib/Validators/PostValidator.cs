using ClubSystem.Lib.Models.Dtos;
using FluentValidation;

namespace ClubSystem.Lib.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.UserIds).NotEmpty();
        }
    }
}
