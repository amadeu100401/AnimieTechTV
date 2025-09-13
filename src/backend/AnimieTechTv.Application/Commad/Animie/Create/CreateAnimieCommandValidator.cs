using AnimieTechTv.Exceptions;
using FluentValidation;

namespace AnimieTechTv.Application.Commad.Animie.Create;

public class CreateAnimieCommandValidator : AbstractValidator<CreateAnimieCommand>
{
    public CreateAnimieCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ResourceMessageExceptions.ANIMIE_NAME_IS_REQUIRED)
            .MaximumLength(100).WithMessage(ResourceMessageExceptions.ANIMIE_CHARACTER);
        RuleFor(x => x.Director)
            .NotEmpty().WithMessage(ResourceMessageExceptions.ANIMIE_DIRECTOR_NAME_REQUIRED)
            .MaximumLength(100).WithMessage(ResourceMessageExceptions.ANIMIE_DIRECTOR_NAME_CHARACTER);
        RuleFor(x => x.Resume)
            .MaximumLength(1000).WithMessage(ResourceMessageExceptions.ANIMIE_RESUME_CHARACTER);
    }
}
