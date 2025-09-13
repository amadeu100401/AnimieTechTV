using AnimieTechTv.Exceptions.ExceptionsBase;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimieTechTv.Application;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .Select(f => f.ErrorMessage)
                .ToList();

            if (failures.Count != 0)
            {
                foreach (var failure in failures)
                    _logger.LogError(failure);

                throw new ErrorOnValidation(failures);
            }
             
        }

        return await next();
    }

}
