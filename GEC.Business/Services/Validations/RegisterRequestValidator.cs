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
            RuleFor(r => r.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(r => r.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
            RuleFor(r => r.Phone)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(14);
            RuleFor(r => r.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();
            RuleFor(r => r.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .PasswordRule();
            RuleFor(r => r.Status)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(value => value == "Active" | value == "InActive")
                .WithMessage("The 'Status' value must be exactly 'Active' or 'InActive'.");;
        }
    }
}