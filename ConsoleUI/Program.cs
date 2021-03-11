using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entites.Concrete;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //carDeleteTest();

            //getCarsBtBrandAndColorId(carManeger);

            //addedCars(brandManeger);

            //addedColors(colorManeger);
            //BrandTest();
            CarManeger carManeger = new CarManeger(new EfCarDal());
            foreach (var car in carManeger.GetCarDetails().Data)
            {
                System.Console.WriteLine(car.CarName+" / "+car.BrandName);
            }
        }


        private static void BrandTest()
        {
            BrandManeger brandManeger = new BrandManeger(new EfBrandDal());
            foreach (var brand in brandManeger.GetAll().Data)
            {
                System.Console.WriteLine(brand.BrandName);
            }
        }

        private static void carDeleteTest()
        {
            CarManeger carManeger = new CarManeger(new EfCarDal());
            BrandManeger brandManeger = new BrandManeger(new EfBrandDal());
            ColorManeger colorManeger = new ColorManeger(new EfColorDal());
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

        private static void addedColors(ColorManeger colorManeger)
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

        private static void addedCars(BrandManeger brandManeger)
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

        private static void getCarsBtBrandAndColorId(CarManeger carManeger)
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
