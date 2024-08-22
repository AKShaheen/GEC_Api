using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Requests;
using GEC.Business.Extensions;

namespace GEC.Business.Services.Validations
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(Name => Name.Name).NotEmpty();
            RuleFor(Phone => Phone.Phone).NotEmpty();
            RuleFor(Email => Email.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).PasswordRule();
        }
    }
}