using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GEC.Business.Contracts.Dtos;
using GEC.Business.Contracts.Requests;
using GEC.Business.Extensions;

namespace GEC.Business.Services.Validations
{
    public class UpdateProductValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(product => product.Name)
                .Cascade(CascadeMode.Stop)
                .InputRule(50);
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