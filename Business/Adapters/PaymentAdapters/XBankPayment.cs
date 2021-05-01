using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Adapters.PaymentAdapters
{
    public class XBankPayment : IBankPosService
    {
        public IResult Pay(CreditCardExtend creditCard, decimal amount)
        {
            //Şifrelenmiş olarak iletilir
            var cardNumber = creditCard.CardNumber.Replace(" ", "");
            XBankFaraziPaymentSystem xBankFaraziPaymentSystem = new XBankFaraziPaymentSystem();
            var result = xBankFaraziPaymentSystem.PaymentSystem(
                cardNumber,
                creditCard.CardHolder,
                creditCard.ExpYear,
                creditCard.ExpMonth,
                creditCard.Cvv, amount);
            if (result.ContainsKey("success"))
            {
                if (result["success"] == "success")
                {
                    return new SuccessResult(result["message"]);
                }
                else
                {
                    return new ErrorResult(result["message"]);
                }
            }
            else
            {
                return new ErrorResult();
            }
        }
    }

    //Banka apisinde yer alan ödeme sistemi simülasyonu (Normalde burada değil, banka apisinde yer alıyor) - SMS gönderimi eksik :)
    public class XBankFaraziPaymentSystem
    {
        //Örnek kartlar
        List<XBankCard> _cards = new List<XBankCard>()
        {
                new XBankCard() {CardId = 1, CardNumber = "4894554325580030", CardHolder = "JOHN SMITH", ExpirationYear = "2020", ExpirationMonth = "11", Cvv = "771", AvailableLimit = 512.12m},
                new XBankCard() {CardId = 2, CardNumber = "5549605095686021", CardHolder = "JANE DOE", ExpirationYear = "2021", ExpirationMonth = "02", Cvv = "423", AvailableLimit = 1675.00m},
                new XBankCard() {CardId = 3, CardNumber = "4475056018091405", CardHolder = "MICHAEL JAEL", ExpirationYear = "2021", ExpirationMonth = "04", Cvv = "268", AvailableLimit = 33.42m},
                new XBankCard() {CardId = 4, CardNumber = "4896710544369175", CardHolder = "LINDA MINA", ExpirationYear = "2023", ExpirationMonth = "08", Cvv = "804", AvailableLimit = 45476.89m},
                new XBankCard() {CardId = 5, CardNumber = "4920365481084617", CardHolder = "BATMAN", ExpirationYear = "2022", ExpirationMonth = "01", Cvv = "130", AvailableLimit = 0.55m},
                new XBankCard() {CardId = 6, CardNumber = "6761246013978692", CardHolder = "SPIDERMAN", ExpirationYear = "2022", ExpirationMonth = "01", Cvv = "777", AvailableLimit = 78635.10m},
                new XBankCard() {CardId = 7, CardNumber = "5170404878973517", CardHolder = "SUPERMAN", ExpirationYear = "2023", ExpirationMonth = "02", Cvv = "541", AvailableLimit = 1789.48m},
                new XBankCard() {CardId = 8, CardNumber = "6364730111104487", CardHolder = "MARLON BRANDO", ExpirationYear = "2024", ExpirationMonth = "03", Cvv = "032", AvailableLimit = 135746.33m},
                new XBankCard() {CardId = 9, CardNumber = "8691005000077342", CardHolder = "GEORGE BORCH", ExpirationYear = "2021", ExpirationMonth = "04", Cvv = "101", AvailableLimit = 1670.00m},
                new XBankCard() {CardId = 10, CardNumber = "5170414892267044", CardHolder = "BLACK BROWN", ExpirationYear = "2020", ExpirationMonth = "05", Cvv = "315", AvailableLimit = 456.17m},
                new XBankCard() {CardId = 11, CardNumber = "4475048941745546", CardHolder = "CHARLIE BROWN", ExpirationYear = "2025", ExpirationMonth = "06", Cvv = "123", AvailableLimit = 688.55m},
                new XBankCard() {CardId = 12, CardNumber = "4603460017289108", CardHolder = "ALBERT EINSTEIN", ExpirationYear = "2022", ExpirationMonth = "07", Cvv = "621", AvailableLimit = 130.40m}
        };

        public Dictionary<string, string> PaymentSystem(string cardNumber, string cardHolder, string expirationYear,
            string expirationMonth,
            string cvv, decimal amount)
        {
            Dictionary<string, string> _result = new Dictionary<string, string>();

            bool dateValidation = true;
            if (Convert.ToInt32(expirationYear) < DateTime.Now.Year)
            {
                dateValidation = false;
            }
            else
            {
                if ((Convert.ToInt32(expirationMonth) < DateTime.Now.Month) &&
                    (Convert.ToInt32(expirationYear) == DateTime.Now.Year))
                {
                    dateValidation = false;
                }
            }

            if (!dateValidation)
            {
                _result.Add("success", "error");
                _result.Add("message", "Credit card expired");
                return _result;
            }

            var cardResult = _cards.Find(card =>
                card.CardNumber == cardNumber
                && card.CardHolder == cardHolder
                && card.ExpirationYear == expirationYear
                && card.ExpirationMonth == expirationMonth
                && card.Cvv == cvv
                );
            if (cardResult == null)
            {
                _result.Add("success", "error");
                _result.Add("message", "Credit card information doesn't match system records.");
            }
            else
            {
                if (cardResult.AvailableLimit < amount)
                {
                    _result.Add("success", "error");
                    _result.Add("message", "The balance is insufficient.");
                }
                else
                {
                    cardResult.AvailableLimit -= amount;
                    _result.Add("success", "success");
                    _result.Add("message", "Payment transaction has been completed.");
                }
            }
            return _result;
        }
    }

    //Banka apisinde yer alan kart bilgileri sınıfı
    public class XBankCard
    {
        public int CardId { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationYear { get; set; }
        public string ExpirationMonth { get; set; }
        public string Cvv { get; set; }
        public decimal AvailableLimit { get; set; }
    }
}
