using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBankPosService
    {
        IResult Pay(CreditCardExtend creditCard, decimal amount);
    }
}
