using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Requests;

namespace GEC.Business.Services.Validations
{
    public class AddProductValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductValidator()
        {
            RuleFor(product => product.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(product => product.Description)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(150);
            RuleFor(product => product.Price)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
            RuleFor(product => product.Stock)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
            RuleFor(product => product.Status)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}