using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace GEC.Business.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static void PasswordRule<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 10)
        {
            ruleBuilder
                .MinimumLength(minimumLength)
                .WithMessage($"Make The Password Bigger than {minimumLength}")
                .Matches("[a-z]")
                .WithMessage("You need to have at least one lower case letter")
                .Matches("[A-Z]")
                .WithMessage("You need to have at least one Upper case letter")
                .Matches("[0-9]")
                .WithMessage("You need to have at least one Number")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("You need to have at least one Special Character");
        }
    }
}