using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Entities.DTOs.RentalDTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapDatabaseContext>, IRentalDal
    {
        public List<GetRentalDetailDTO> GetRentalDetails()
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                var result = (from rent in context.Rentals
                              join customer in context.Users
                              on rent.CustomerId equals customer.Id
                              join car in context.Cars
                              on rent.CarId equals car.CarId
                              select new GetRentalDetailDTO
                              {
                                  CarName = car.CarName,
                                  CustomerName = customer.FirstName + " " + customer.LastName,
                                  Id = rent.Id,
                                  RentDate = rent.RentDate,
                                  ReturnDate = rent.ReturnDate
                              }
                              ).ToList();
                return result;
            }
        }

        public bool IsCarAvailable(int id)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                var result = context.Rentals.Any(x => x.Id == id && x.ReturnDate == null);
                return result;
            }
        }
    }
}
