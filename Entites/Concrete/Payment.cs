namespace Entities.Concrete
{
    public class Payment
    {
        public int RentalId { get; set; }
        public CreditCardExtend PayCard { get; set; }
        public bool IsSave { get; set; }

    }
}
