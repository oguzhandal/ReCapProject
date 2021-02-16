using Business.Concrete;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManeger carManeger = new CarManeger(new InMemoryCarDal());
            foreach (var item in carManeger.GetAll())
            {
                System.Console.WriteLine("carId: " + item.CarId + "| DailyPrice: " + item.DailyPrice + "|  ModelYear: " + item.ModelYear + "| Description: " + item.Description);
            }
        }
    }
}
