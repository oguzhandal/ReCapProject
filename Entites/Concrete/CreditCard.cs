using Core.Entities;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int  Id { get; set; }
        public int CustomerId { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string ExpYear { get; set; }
        public string ExpMonth { get; set; }
    }
}
