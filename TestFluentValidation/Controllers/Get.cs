using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestFluentValidation.Domain;
using TestFluentValidation.Validators;

namespace TestFluentValidation.Controllers
{
    public class Get
    {
        #region Query
        public class Query : IRequest<Model>
        {
            internal string Email { get; set; }
        }
        #endregion

        #region Validator
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
                => RuleFor(x => x.Email)
                    .NotNull()
                    .WithState(x => new Error("Get.Validator", "Email is null"))
                    .SetValidator(new EmailValidator(false));
        }
        #endregion

        #region Model
        public class Model
        {
            public string Result { get; set; }

            public Model() { }
            public Model(string res) => Result = res;
        }
        #endregion

        #region Handler
        public class Handler : IRequestHandler<Query, Model>
        {
            public Handler()
            {
            }

            public async Task<Model> Handle(Query request, CancellationToken token)
            {
                var email = Email.Create(request.Email);

                return new Model($"{email} is {(email.IsGmail ? "" : "not ")}gmail");
            }
        }
        #endregion
    }
}
