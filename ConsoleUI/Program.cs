﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entites.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //carDeleteTest
            //getCarsBtBrandAndColorId(carManeger
            //addedCars(brandManeger);
            //addedColors(colorManeger);
            //BrandTest();
            //GetCarDetailsTest();

            //UserAdd();
            newRentalAdd();

        }

        private static void newRentalAdd()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            rentalManager.Add(new Rental { CarId = 5, CustomerId = 1, RentDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(7) });
            rentalManager.Add(new Rental { CarId = 5, CustomerId = 2, RentDate = DateTime.Now, ReturnDate =DateTime.Now.AddDays(1) });
            rentalManager.Add(new Rental { CarId = 5, CustomerId = 3, RentDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(2) });

        }

        private static void UserAdd()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            List<User> userList = new List<User>
            {
                new User { FirstName = "Dünya", LastName = "dal", Email = "Dunya@gmail.com", Password = "DunyaDal" },
                new User{FirstName="Bulut",LastName="yılmaz",Email="bulutyılmaz@hotmail.com",Password="bulutyılmaz"},
                new User{FirstName="Deniz",LastName="Özgür",Email="DOzgur@hotmail.com",Password="ozgurDeniz"},
                new User{FirstName="Özge",LastName="Doğan",Email="OzgeDogan@hotmail.com",Password="OzgeDogan"},


            };
            foreach (var user in userList)
            {
                userManager.Add(user);
                System.Console.WriteLine("Kullanıcı + " + user.FirstName + " " + user.LastName + " eklendi");

            }
        }

        private static void GetCarDetailsTest()
        {
            CarManager carManeger = new CarManager(new EfCarDal());
            foreach (var car in carManeger.GetCarDetails().Data)
            {
                System.Console.WriteLine(car.CarName + " / " + car.BrandName);
            }
        }

        private static void BrandTest()
        {
            BrandManager brandManeger = new BrandManager(new EfBrandDal());
            foreach (var brand in brandManeger.GetAll().Data)
            {
                System.Console.WriteLine(brand.BrandName);
            }
        }

        private static void carDeleteTest()
        {
            CarManager carManeger = new CarManager(new EfCarDal());
            BrandManager brandManeger = new BrandManager(new EfBrandDal());
            ColorManager colorManeger = new ColorManager(new EfColorDal());
            Car car1 = new Car()
            {
                CarId = 2,
                ColorId = 4,
                BrandId = 1,
                CarName = "Audi",
                ModelYear = 2006,
                DailyPrice = 150,
                Description = "0/100: 9.8 saniye, Yakıt:Benzin"
            };

            carManeger.Delete(car1);
        }

        private static void addedColors(ColorManager colorManeger)
        {
            //consol da kısa yoldan renk girmek için yazıldı
            for (int i = 1; i <= 10; i++)
            {
                System.Console.WriteLine("Renk giriniz");
                string colorName = System.Console.ReadLine();
                Color color = new Color() { ColorId = i, ColorName = colorName };
                colorManeger.Add(color);
            }
        }

        private static void addedCars(BrandManager brandManeger)
        {
            //Sıralı bir şekilde kendi istediğim kadar tabloya Araba modeli ekledim
            for (int i = 1; i <= 10; i++)
            {
                System.Console.WriteLine("marka giriniz");
                string brandName = System.Console.ReadLine();
                Brand brand = new Brand() { BrandId = i, BrandName = brandName };
                brandManeger.Add(brand);
            }
        }

        private static void getCarsBtBrandAndColorId(CarManager carManeger)
        {
            foreach (var item in carManeger.GetCarsByBrandId(1).Data)
            {
                System.Console.WriteLine("Car name: " + item.CarName + " BrandId: " + item.BrandId);
            }
            foreach (var item in carManeger.GetCarsByColorId(1).Data)
            {
                System.Console.WriteLine("Car Name " + item.CarName + " colorId: " + item.ColorId);
            }
        }
    }
}
