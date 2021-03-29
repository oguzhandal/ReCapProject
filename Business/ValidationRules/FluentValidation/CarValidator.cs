using Entites.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.CarName).MinimumLength(2);
            RuleFor(c => c.DailyPrice).GreaterThan(0);
            //RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(500).When(c => c.BrandId == 1);
            //RuleFor(c => c.CarName).Must(StartWithA).WithMessage("A harfi ile başlamalı");
        }

        private bool StartWithA(string arg)
        {
            //arg ==c.carname
            return arg.StartsWith("A");
        }
    }
}
