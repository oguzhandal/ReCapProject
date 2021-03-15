using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapDatabaseContext>, ICustomerDal
    {

        public List<CarCustomerDetailDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                var result = from cus in filter is null ? context.Customers : context.Customers.Where(filter)
                             join usr in context.Users on cus.UserId equals usr.Id
                             select new CarCustomerDetailDto
                             {
                                 Id = cus.Id,
                                 UserId = usr.Id,
                                 UserName = usr.FirstName,
                                 UserLastName = usr.LastName,
                                 CompanyName = cus.CompanyName
                             };
                return result.ToList();
            }
        }

    }
}
