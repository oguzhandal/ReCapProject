using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IRentalService _rentalService;
        ICreditCardService _creditCardService;
        IBankPosService _bankPosService;

        public PaymentManager(ICreditCardService creditCardService, IRentalService rentalService, IBankPosService bankPosService)
        {
            _creditCardService = creditCardService;
            _rentalService = rentalService;
            _bankPosService = bankPosService;
        }

        [ValidationAspect(typeof(CreditCardExtendValidator))]
        [TransactionScopeAspect]
        public IResult Pay(Payment payment)
        {

            IDataResult<Rental> rentalResult = _rentalService.GetById(payment.RentalId);
            if (!rentalResult.Success)
            {
                return new ErrorResult(rentalResult.Message);
            }

            if (rentalResult.Data.PayConfirm)
            {
                return new ErrorResult(Messages.ExistPayConfirm);
            }

            var result = _bankPosService.Pay(payment.PayCard, rentalResult.Data.Amount);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            rentalResult.Data.PayConfirm = true;
            result = _rentalService.Update(rentalResult.Data);
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            if (payment.IsSave)
            {
                CreditCard newCard = new CreditCard()
                {
                    CardHolder = payment.PayCard.CardHolder,
                    CardNumber = payment.PayCard.CardNumber,
                    CustomerId = payment.PayCard.CustomerId,
                    ExpYear = payment.PayCard.ExpYear,
                    ExpMonth = payment.PayCard.ExpMonth
                };
                result = _creditCardService.Add(newCard);
                if (!result.Success)
                {
                    return new ErrorResult(result.Message);
                }
            }
            return new SuccessResult(Messages.PaymentComplete);
        }
    }
}
