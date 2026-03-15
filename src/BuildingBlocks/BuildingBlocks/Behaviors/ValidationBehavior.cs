using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System.Net.Http.Headers;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> 
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validtionResults = await Task.WhenAll(validators.Select(c => c.ValidateAsync(context, cancellationToken)));

            var failures = validtionResults.Where(c => c.Errors.Any()).SelectMany(c => c.Errors).ToList();

            if (failures.Any()) throw new ValidationException(failures);

            return await next();
        }
    }
}
