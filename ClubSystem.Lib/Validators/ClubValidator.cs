using ClubSystem.Lib.Models.Entities;
using FluentValidation;

namespace ClubSystem.Lib.Validators
{
    public class ClubValidator : AbstractValidator<Club>
    {
        public ClubValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UniversityName).NotEmpty();
            RuleFor(x => x.UserClubs).NotEmpty();
        }
    }
}
