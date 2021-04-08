using Core.DataAccess;
using Entites.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailsDTO> GetCarDetails(Expression<Func<Car, bool>> filter = null);

    }
}
