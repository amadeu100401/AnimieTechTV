using AnimieTechTv.Exceptions;
using FluentValidation;

namespace AnimieTechTv.Application.Commad.Animie.Update;

public class UpdateAnimieInfoValidator : AbstractValidator<UpdateAnimieInfoCommand>
{
    public UpdateAnimieInfoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ResourceMessageExceptions.ANIMIE_ID_REQUIRED);
        RuleFor(x => x.Name)
            .MaximumLength(200)
            .WithMessage(ResourceMessageExceptions.ANIMIE_CHARACTER);
        RuleFor(x => x.Director)
            .MaximumLength(100)
            .WithMessage(ResourceMessageExceptions.ANIMIE_DIRECTOR_NAME_CHARACTER);
        RuleFor(x => x.Resume)
            .MaximumLength(1000)
            .WithMessage(ResourceMessageExceptions.ANIMIE_RESUME_CHARACTER);
    }
}
