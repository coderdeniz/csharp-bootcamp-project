using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;

ICarService carService = new CarManager(new InMemoryCarDal());

foreach (var car in carService.GetAll())
{
    Console.WriteLine("araç id:" + car.Id + " - " + "marka:" + car.BrandId + " - " + car.DailyPrice + "TL");
}
