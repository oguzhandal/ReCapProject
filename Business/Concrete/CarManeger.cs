using Business.Abstract;
using DataAccess.Abstract;
using Entites.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManeger : ICarService
    {
        ICarDal _carDal;

        public CarManeger(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if (car.Name.Length < 2 && car.DailyPrice < 0)
            {
                Console.WriteLine("Araba eklerken HATA oluştu Araba adı minimum 2 karakter ve Günlük üceti 0 dan büyük olmalıdır.");
            }
            else
            {
                _carDal.Add(car);
                Console.WriteLine("Araba başarıyla eklendi.");
            }
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
            Console.WriteLine("Araba başarıyla silindi");
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
            Console.WriteLine("Başarıyla güncellendi");
        }
        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll(item => item.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll(item => item.ColorId == colorId);
        }
    }
}
