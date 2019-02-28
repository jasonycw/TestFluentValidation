using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TestFluentValidation.Domain;

namespace TestFluentValidation.Validators
{
    public static class ValidatorExtensions
    {
        public static void EmailAddress<T>(this IRuleBuilder<T, string> rule, bool acceptTecEmail)
        {
            rule.Must(x => Email.Create(x) != null)
                .WithState(x => new Error("EmailValidator", $"{x} is null"));
            rule.Must(x => Email.Create(x)?.IsGmail ?? false)
                .When(x => !acceptTecEmail)
                .WithState(x => new Error("EmailValidator", $"{Email.Create(x)} is null"));

        }
    }
}
