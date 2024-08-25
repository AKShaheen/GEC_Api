using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Requests;

namespace GEC.Business.Services.Validations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {    
            RuleFor(email => email.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();
            RuleFor(password => password.Password)
                .NotEmpty();
        }
    }
}