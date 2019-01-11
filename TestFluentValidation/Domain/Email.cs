using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TestFluentValidation.Domain
{
    public class Email
    {
        public string Value { get; private set; }
        public bool IsGmail => Value.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);

        private Email(string value) => Value = value;

        public override string ToString() => Value;

        #region Static methods
        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                return null;
            try
            {
                return new Email(new MailAddress(value).Address.ToLowerInvariant());
            }
            catch { /* ignored */ }
            return null;
        }
        #endregion
    }
}
