using AnimieTechTv.Exceptions;
using FluentValidation;

namespace AnimieTechTv.Application.Commad.Animie.Get;

public class GetAnimieValidatior : AbstractValidator<GetAnimieCommand>
{
    public GetAnimieValidatior()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage(ResourceMessageExceptions.ANIMIE_CHARACTER);
        RuleFor(x => x.Director)
            .MaximumLength(100).WithMessage(ResourceMessageExceptions.ANIMIE_DIRECTOR_NAME_CHARACTER);
    }
}
