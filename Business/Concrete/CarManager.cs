using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entites.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICarImageService _carImageService;
        string _defaultImagePath = @"\Images\DefaultCarImage.jpg";

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }


        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("car.add,admin,editor")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.NewCarAdded);
        }

        [SecuredOperation("car.update,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            //IResult result = Validate(car);
            //if (!result.Success)
            //{
            //    return new ErrorResult(result.Message);
            //}

            //var result = ValidationTool.Validate(new CarValidator(), car);
            //if (!result.Success)
            //{
            //    return new ErrorResult(Messages.CarIsNotValid);
            //}

            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            var fileFolderPath = AppContext.BaseDirectory + @"Images";
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [SecuredOperation("car.delete,admin")]
        [CacheRemoveAspect("ICarService.Get")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            var images = _carImageService.GetByCarId(car.CarId).Data;
            foreach (var image in images)
            {
                _carImageService.Delete(image);
            }
            return new SuccessResult(Messages.CarDeleted);
        }


        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<CarDetailDto>> GetAllCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetails());
        }

        [CacheAspect]
        public IDataResult<CarDetailDto> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetDetails(c => c.CarId == carId, _defaultImagePath));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetailsBy(c => c.ColorId == colorId, _defaultImagePath));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetailsBy(c => c.BrandId == brandId, _defaultImagePath));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandAndColorId(int brandId, int colorId)
        {
            Expression<Func<Car, Boolean>> filter;
            if (colorId == 0 && brandId == 0)
            {
                return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetails(_defaultImagePath));
            }
            else if (colorId > 0 && brandId > 0)
            {
                filter = c => c.BrandId == brandId && c.ColorId == colorId;
            }
            else if (brandId > 0)
            {
                filter = c => c.BrandId == brandId;
            }
            else
            {
                filter = c => c.ColorId == colorId;
            }

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllDetailsBy(filter, _defaultImagePath));
        }

        [CacheAspect]
        public IDataResult<int> Count()
        {
            return new SuccessDataResult<int>(_carDal.GetAll().Count);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetRentableCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetRentableDetails());
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetRentedCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetRentedDetails());
        }

        [CacheAspect]
        [PerformanceAspect(10)]
        public IDataResult<List<CarDetailWithImagesDto>> GetAllCarDetailsWithImages()
        {
            return new SuccessDataResult<List<CarDetailWithImagesDto>>(_carDal.GetAllDetailsWithImages(_defaultImagePath));
        }

        [CacheAspect]
        public IDataResult<CarDetailWithImagesDto> GetCarDetailsByIdWithImages(int carId)
        {
            return new SuccessDataResult<CarDetailWithImagesDto>(_carDal.GetDetailsWithImagesById(carId, _defaultImagePath));
        }

        //public IResult Validate(Car car)
        //{
        //    bool result = (NameValidate(car).Success && DailyPriceValidate(car).Success);
        //    string message = NameValidate(car).Message + "\n" + DailyPriceValidate(car).Message;
        //    if (result)
        //    {
        //        return new SuccessResult(message);
        //    }
        //    else
        //    {
        //        return new ErrorResult(message);
        //    }
        //}

        //private IResult DailyPriceValidate(Car car)
        //{
        //    if (car.DailyPrice > 0)
        //    {
        //        return new SuccessResult();
        //    }
        //    else
        //    {
        //        return new ErrorResult(Messages.DailyPriceGreater);
        //    }
        //}

        //private IResult NameValidate(Car car)
        //{
        //    if (car.CarName.Length >= 2)
        //    {
        //        return new SuccessResult();
        //    }
        //    else
        //    {
        //        return new ErrorResult(Messages.CarNameLeast);
        //    }
        //}
    }
}