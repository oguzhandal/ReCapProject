using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.RentalDTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        bool IsCarAvailable(int id);
        List<GetRentalDetailDTO> GetRentalDetails();
    }
}
