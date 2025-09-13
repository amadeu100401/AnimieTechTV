using AnimieTechTv.Exceptions;
using FluentValidation;

namespace AnimieTechTv.Application.Commad.Animie.Delete;

public class DeleteAnimieValidator : AbstractValidator<DeleteAnimieCommand>
{
    public DeleteAnimieValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ResourceMessageExceptions.ANIMIE_ID_REQUIRED)
            .Must(id => id != Guid.Empty)
            .WithMessage(ResourceMessageExceptions.ID_MUST_BE_GUID);
    }
}
