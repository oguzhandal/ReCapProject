using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapDatabaseContext>, IRentalDal
    {
        public List<CarRentalDetailDto> GetCarRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                var result = from rnt in filter is null ? context.Rentals : context.Rentals.Where(filter)
                             join cr in context.Cars on rnt.CarId equals cr.CarId
                             join col in context.Colors on cr.ColorId equals col.ColorId
                             join brd in context.Brands on cr.BrandId equals brd.BrandId
                             join cus in context.Customers on rnt.CustomerId equals cus.Id
                             join usr in context.Users on cus.UserId equals usr.Id
                             select new CarRentalDetailDto
                             {
                                 RentalId = rnt.Id,
                                 CustomerName = usr.FirstName,
                                 CustomerLastName = usr.LastName,
                                 CustomerCompanyName = cus.CompanyName,
                                 CarName = cr.Description,
                                 BrandName = brd.BrandName,
                                 ColorName = col.ColorName,
                                 DailyPrice = cr.DailyPrice,
                                 RentDate = rnt.RentDate,
                                 ReturnDate = rnt.ReturnDate
                             };

                return result.ToList();
            }
        }
    }
}
