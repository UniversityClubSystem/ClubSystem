using ClubSystem.Lib.Models.Dtos;
using FluentValidation;

namespace ClubSystem.Lib.Validators
{
    public class ClubValidator : AbstractValidator<ClubDto>
    {
        public ClubValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UniversityName).NotEmpty();
        }
    }
}
