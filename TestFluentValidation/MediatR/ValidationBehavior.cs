using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TestFluentValidation.Domain;

namespace TestFluentValidation.MediatR
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _factory;

        public ValidationBehavior(IValidatorFactory factory) => _factory = factory;

        public async Task<TResponse> Handle(TRequest request, CancellationToken token, RequestHandlerDelegate<TResponse> next)
        {
            if (request == null)
                return await next();

            var requestType = request.GetType();
            foreach (var t in new[] { requestType }.Concat(requestType.GetInterfaces()).Distinct())
            {
                var validation = await (
                    _factory.GetValidator(t)?.ValidateAsync(request, token) ??
                    Task.FromResult(new ValidationResult()));

                if (validation.IsValid) continue;

                var error = validation.Errors.First();
                return !(error.CustomState is Error err)
                    ? throw new NotSupportedException($"Validation state must be of type '{nameof(Error)}'.")
                    : (TResponse)Activator.CreateInstance(typeof(TResponse), err.ToString());
            }
            return await next();
        }
    }
}
