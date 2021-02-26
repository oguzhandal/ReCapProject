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
            CarManeger carManeger = new CarManeger(new EfCarDal());
            BrandManeger brandManeger = new BrandManeger(new EfBrandDal());
            ColorManeger colorManeger = new ColorManeger(new EfColorDal());
            Car car1 = new Car() {
                CarId = 2,
                ColorId = 10,
                BrandId = 5,
                Name="Citroen",
                ModelYear = 2006,
                DailyPrice = 200,
                Description = "Beygir:110, 0/100: 9.8 saniye, Yakıt:Dizel" };
            
            carManeger.Add(car1);


            //foreach (var item in carManeger.GetCarsByBrandId(1))
            //{
            //    System.Console.WriteLine("Car name: " + item.Name + " BrandId: " + item.BrandId);
            //}
            //foreach (var item in carManeger.GetCarsByColorId(1))
            //{
            //    System.Console.WriteLine("Car Name " + item.Name + " colorId: " + item.ColorId);
            //}

            //Sıralı bir şekilde kendi istediğim kadar tabloya Araba modeli ekledim
            //for (int i = 1; i <= 10; i++)
            //{
            //    System.Console.WriteLine("marka giriniz");
            //    string brandName = System.Console.ReadLine();
            //    Brand brand = new Brand() { BrandId = i, BrandName = brandName };
            //    brandManeger.Add(brand);
            //}

            //consol da kısa yoldan renk girmek için yazıldı
            //for (int i = 1; i <= 10; i++)
            //{
            //    System.Console.WriteLine("Renk giriniz");
            //    string colorName = System.Console.ReadLine();
            //    Color color = new Color() { ColorId = i, ColorName = colorName };
            //    colorManeger.Add(color);

            //}
        }
    }
}
