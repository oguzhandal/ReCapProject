using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CreditCardExtendValidator : AbstractValidator<CreditCardExtend>
    {
        public CreditCardExtendValidator()
        {
            RuleFor(card => card.ExpYear)
                            .NotEmpty()
                            .WithMessage("Credit card expiry year is required")
                            .Must(x => Convert.ToInt32(x) >= DateTime.Now.Year)
                            .WithMessage("The credit card expiry year is invalid");

            RuleFor(card => card.ExpMonth)
                .NotEmpty()
                .WithMessage("Credit card expiry month is required")
                .Must(x => Convert.ToInt32(x) >= DateTime.Now.Month)
                .WithMessage("The credit card expiry month is invalid")
                .When(card => Convert.ToInt32(card.ExpYear) == DateTime.Now.Year);
            RuleFor(card => card.CardNumber).CreditCard();
        }
    }
}
