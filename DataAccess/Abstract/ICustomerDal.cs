using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        List<CustomerDetailDto> GetAllDetails();
        List<CustomerDetailDto> GetAllDetailsBy(Expression<Func<Customer, bool>> filter);
        CustomerDetailDto GetDetails(Expression<Func<Customer, bool>> filter);
    }
}
