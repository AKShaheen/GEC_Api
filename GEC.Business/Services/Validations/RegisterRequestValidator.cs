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
            RuleFor(Name => Name.Name).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(Phone => Phone.Phone).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(Email => Email.Email).Cascade(CascadeMode.Stop).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty().PasswordRule();
        }
    }
}