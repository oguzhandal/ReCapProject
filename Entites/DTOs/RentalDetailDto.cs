using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        [Key]
        public int Id { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Amount { get; set; }
        public bool PayConfirm { get; set; }
    }
}
