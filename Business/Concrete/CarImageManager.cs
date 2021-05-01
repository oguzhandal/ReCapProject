using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;

        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());

        }

        [CacheAspect]
        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        [SecuredOperation("Car.add,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            List<IResult> errorResult = BusinessRules.Run(CheckIfCarImageLimitExceeded(carImage.CarId));
            if (errorResult.Count == 0)
            {
                carImage.ImagePath = FileHelper.Add(file).Data;
                carImage.Date = DateTime.Now;
                _carImageDal.Add(carImage);
                return new SuccessResult();
            }
            string message = "";
            foreach (IResult result in errorResult)
            {
                message += result.Message + "\n";
            }
            return new ErrorResult(message);

        }

        [SecuredOperation("car.update,admin,editor")]
        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Update(_carImageDal.Get(c => c.Id == carImage.Id).ImagePath, file).Data;
            _carImageDal.Update(carImage);
            return new SuccessResult();

        }
        [SecuredOperation("car.delete,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(CheckAndSetDefaulltImage(carId));
        }

        private IResult CheckIfCarImageLimitExceeded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (carImageCount >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }

            return new SuccessResult();
        }

        private List<CarImage> CheckAndSetDefaulltImage(int carId)
        {
            string path = Environment.CurrentDirectory + @"\images\DefaultCarImage.jpg";
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();

            if (result)
            {
                return _carImageDal.GetAll(c => c.CarId == carId);
            }

            return new List<CarImage> { new CarImage { CarId = carId, ImagePath = path, Date = DateTime.Now } };
        }

    }
}
