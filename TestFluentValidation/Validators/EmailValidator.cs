using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using TestFluentValidation.Domain;

namespace TestFluentValidation.Validators
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator(bool acceptGmail = true)
        {
            RuleFor(x => x)
                .NotNull()
                .WithState(x => new Error("EmailValidator", "Email is null"));
            RuleFor(x => Email.Create(x))
                .NotNull()
                .WithState(x => new Error("EmailValidator", "Email is not valid"));
            RuleFor(x => Email.Create(x))
                .Must(x => !(x?.IsGmail ?? false))
                .When(x => !acceptGmail)
                .WithState(x => new Error("EmailValidator", "Gmail is not accepted"));
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<string> context, CancellationToken cancellation = new CancellationToken())
        {
            if (context == null)
                throw new ValidationException("Email cannot be null");
            return base.ValidateAsync(context, cancellation);
        }

        protected override void EnsureInstanceNotNull(object instanceToValidate)
        {
            if (instanceToValidate == null)
                throw new ValidationException("Email cannot be null");
        }
    }
}
