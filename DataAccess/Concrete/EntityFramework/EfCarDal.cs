using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entites.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapDatabaseContext>, ICarDal
    {
        public List<CarDetailDto> GetAllDetails(string defaultImagePath)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                var carImages = from cImages in context.CarImages
                                select cImages;

                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 Images = !(carImages.Any(cim => cim.CarId == c.CarId))
                                     ? new List<CarImage>() { new CarImage { Id = 0, CarId = c.CarId, Date = DateTime.Now, ImagePath = defaultImagePath } }
                                     : carImages.Where(cim => cim.CarId == c.CarId).ToList()
                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetDetails(Expression<Func<Car, bool>> filter, string defaultImagePath)
        {
            return GetAllDetailsBy(filter, defaultImagePath).SingleOrDefault();
        }

        public List<CarDetailDto> GetAllDetailsBy(Expression<Func<Car, bool>> filter, string defaultImagePath)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                var carImages = from cImages in context.CarImages
                                select cImages;

                var result = from c in context.Cars.Where(filter)
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 Images = !(carImages.Any(cim => cim.CarId == c.CarId))
                                 ? new List<CarImage>() { new CarImage { Id = 0, CarId = c.CarId, Date = DateTime.Now, ImagePath = defaultImagePath } }
                                 : carImages.Where(cim => cim.CarId == c.CarId).ToList()
                             };

                return result.ToList();
            }
        }

        public List<CarDetailDto> GetRentableDetails(string defaultImagePath)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                //var query =
                //    from c in context.Cars
                //    where !(from r in context.Rentals
                //            select r.CarId)
                //           .Contains(c.CarId)
                //           join b in context.Brands
                //           on c.BrandId equals b.Id
                //    select c;
                //var cars = !from r in context.Rentals
                //from c in context.Cars.Where(x => r.CarId.Equals(x.CarId)).ToList();
                //var rentals = (from r in context.Rentals.Contains select r.CarId);

                var carImages = from cImages in context.CarImages
                                select cImages;

                var result =
                    from c in context.Cars
                    where !(from r in context.Rentals
                            select r.CarId)
                    .Contains(c.CarId)
                    join b in context.Brands
                    on c.BrandId equals b.Id
                    join cl in context.Colors
                    on c.ColorId equals cl.Id
                    select new CarDetailDto
                    {
                        CarId = c.CarId,
                        CarName = c.CarName,
                        BrandName = b.Name,
                        ColorName = cl.Name,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        Images = !(carImages.Any(cim => cim.CarId == c.CarId))
                                     ? new List<CarImage>() { new CarImage { Id = 0, CarId = c.CarId, Date = DateTime.Now, ImagePath = defaultImagePath } }
                                     : carImages.Where(cim => cim.CarId == c.CarId).ToList()
                    };
                return result.ToList();
            }

        }

        public List<CarDetailDto> GetRentedDetails(string defaultImagePath)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                //var query =
                //    from c in context.Cars
                //    where !(from r in context.Rentals
                //            select r.CarId)
                //           .Contains(c.CarId)
                //           join b in context.Brands
                //           on c.BrandId equals b.Id
                //    select c;
                //var cars = !from r in context.Rentals
                //from c in context.Cars.Where(x => r.CarId.Equals(x.CarId)).ToList();
                //var rentals = (from r in context.Rentals.Contains select r.CarId);

                var carImages = from cImages in context.CarImages
                                select cImages;

                var result =
                    from c in context.Cars
                    where (from r in context.Rentals
                           select r.CarId)
                    .Contains(c.CarId)
                    join b in context.Brands
                    on c.BrandId equals b.Id
                    join cl in context.Colors
                    on c.ColorId equals cl.Id
                    select new CarDetailDto
                    {
                        CarId = c.CarId,
                        CarName = c.CarName,
                        BrandName = b.Name,
                        ColorName = cl.Name,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        Images = !(carImages.Any(cim => cim.CarId == c.CarId))
                        ? new List<CarImage>() { new CarImage { Id = 0, CarId = c.CarId, Date = DateTime.Now, ImagePath = defaultImagePath } }
                : carImages.Where(cim => cim.CarId == c.CarId).ToList()
                    };
                return result.ToList();
            }
        }

        public List<CarDetailWithImagesDto> GetAllDetailsWithImages(string defaultImagePath)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {

                //var carImages = from c in context.Cars
                //                join ci in context.CarImages
                //                on c.CarId equals ci.CarId
                //                select new { CarId = c.CarId, ImageId = ci.Id, Date = ci.Date, ImagePath = ci.ImagePath };

                var carImages = from cImages in context.CarImages
                                select cImages;

                var result =
                    from c in context.Cars
                    join b in context.Brands
                    on c.BrandId equals b.Id
                    join cl in context.Colors
                    on c.ColorId equals cl.Id
                    join ci in context.CarImages
                    on c.CarId equals ci.CarId
                    select new CarDetailWithImagesDto
                    {
                        CarId = c.CarId,
                        CarName = c.CarName,
                        BrandName = b.Name,
                        ColorName = cl.Name,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        Images = !(carImages.Any(cim => cim.CarId == c.CarId))
                        ? new List<CarImage>() { new CarImage { Id = 0, CarId = c.CarId, Date = DateTime.Now, ImagePath = defaultImagePath } }
                        : carImages.Where(cim => cim.CarId == c.CarId).ToList()
                    };
                var carDetailWithImagesDtos = result.ToList();

                return carDetailWithImagesDtos;
            }
        }

        public CarDetailWithImagesDto GetDetailsWithImagesById(int carId, string defaultImagePath)
        {
            using (ReCapDatabaseContext context = new ReCapDatabaseContext())
            {
                var carImages = from cImages in context.CarImages
                                select cImages;
                var result =
                    from c in context.Cars.Where(car => car.CarId == carId)
                    join b in context.Brands
                    on c.BrandId equals b.Id
                    join cl in context.Colors
                    on c.ColorId equals cl.Id
                    join ci in context.CarImages
                    on c.CarId equals ci.CarId
                    select new CarDetailWithImagesDto
                    {
                        CarId = c.CarId,
                        CarName = c.CarName,
                        BrandName = b.Name,
                        ColorName = cl.Name,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        Images = !(carImages.Any(cim => cim.CarId == c.CarId))
                        ? new List<CarImage>() { new CarImage { Id = 0, CarId = c.CarId, Date = DateTime.Now, ImagePath = defaultImagePath } }
                        : carImages.Where(cim => cim.CarId == c.CarId).ToList()
                    };
                return result.SingleOrDefault(car => car.CarId == carId);
            }
        }
    }
}