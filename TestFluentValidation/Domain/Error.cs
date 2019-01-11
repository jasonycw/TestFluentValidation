using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFluentValidation.Domain
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error(string code, Exception exception)
            : this(code, exception?.Message) { }

        public override string ToString()
            => $"[{Code}] {Message}";
    }
}
