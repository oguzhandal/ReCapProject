using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetById(int İd);
        IResult Add(Color color);
        IResult Delete(Color color);
        IResult Update(Color color);
    }
}
