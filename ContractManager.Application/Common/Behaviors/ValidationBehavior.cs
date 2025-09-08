using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Common.Behaviors;

// Ce "comportement" de pipeline MediatR intercepte chaque requête avant qu'elle n'atteigne son handler.
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            // On exécute tous les validateurs pour la requête actuelle.
            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            // On collecte toutes les erreurs.
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // S'il y a des erreurs, on lève une exception qui sera interceptée par l'API.
            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        // S'il n'y a pas d'erreur, on passe à l'étape suivante (le handler).
        return await next();
    }
}
