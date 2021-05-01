using Core.DataAccess;
using DataAccess.Abstract;
using Entites.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    /* public class InMemoryCarDal : ICarDal
     {
         List<Car> _cars;



         public InMemoryCarDal()
         {
             _cars = new List<Car>
             {
                 new Car {CarId=1,BrandId=1,ColorId=1,DailyPrice=15000,ModelYear=2006,CarName="Mustang",Description="Car Item 1"},
                 new Car {CarId=2,BrandId=2,ColorId=2,DailyPrice=5500,ModelYear=1998,CarName="toyota",Description="Car Item 2"},
                 new Car {CarId=3,BrandId=3,ColorId=3,DailyPrice=10000,ModelYear=2002,CarName="BMW",Description="Car Item 3"},
                 new Car {CarId=4,BrandId=4,ColorId=4,DailyPrice=25000,ModelYear=2012,CarName="Mercedes",Description="Car Item 4"}
             };
         }

         public void Add(Car car)
         {
             _cars.Add(car);
         }

         public void Delete(Car car)
         {
             Car carToDelete = _cars.SingleOrDefault(p => p.CarId == car.CarId);
             _cars.Remove(carToDelete);
         }

         public Car Get(Expression<Func<Car, bool>> filter)
         {
             throw new NotImplementedException();
         }

         public List<Car> GetAll()
         {
             return _cars;
         }

         public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
         {
             throw new NotImplementedException();
         }

         public List<CarDetailDto> GetAllDetails(string defaultImagePath = null)
         {
             throw new NotImplementedException();
         }

         public List<CarDetailDto> GetAllDetailsBy(Expression<Func<Car, bool>> filter, string defaultImagePath = null)
         {
             throw new NotImplementedException();
         }

         public List<CarDetailWithImagesDto> GetAllDetailsWithImages(string defaultImagePath = null)
         {
             throw new NotImplementedException();
         }

         public List<Car> GetById(int carId)
         {
             return _cars.Where(p => p.CarId == carId).ToList();
         }

         public List<CarDetailDTO> GetCarDetails(Expression<Func<Car, bool>> filter = null)
         {
             throw new NotImplementedException();
         }

         public CarDetailDto GetDetails(Expression<Func<Car, bool>> filter, string defaultImagePath = null)
         {
             throw new NotImplementedException();
         }

         public CarDetailWithImagesDto GetDetailsWithImagesById(int carId, string defaultImagePath = null)
         {
             throw new NotImplementedException();
         }

         public List<CarDetailDto> GetRentableDetails(string defaultImagePath = null)
         {
             throw new NotImplementedException();
         }

         public List<CarDetailDto> GetRentedDetails(string defaultImagePath = null)
         {
             throw new NotImplementedException();
         }

         public void Update(Car car)
         {
             Car carToUpdate = _cars.SingleOrDefault(p => p.CarId == car.CarId);
             carToUpdate.BrandId = car.BrandId;
             carToUpdate.CarName = car.CarName;
             carToUpdate.ColorId = car.ColorId;
             carToUpdate.DailyPrice = car.DailyPrice;
             carToUpdate.Description = car.Description;
             carToUpdate.ModelYear = car.ModelYear;

         }

         Car IEntityRepository<Car>.Add(Car entity)
         {
             throw new NotImplementedException();
         }
     }*/
}
