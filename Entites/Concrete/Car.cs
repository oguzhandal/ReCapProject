using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entites.Concrete
{
    public class Car : IEntity
    {
        [Key]
        public int CarId { get; set; }
        public string CarName { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public short ModelYear { get; set; }
        public int DailyPrice { get; set; }
        public string Description { get; set; }

    }
}
