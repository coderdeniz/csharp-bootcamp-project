using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

ICarService carService = new CarManager(new EfCarDal());

//Add(carService);

GetCarsByColorId(carService);



static void Add(ICarService carService)
{
    carService.Add(new Car
    {
        DailyPrice = 750,
        BrandId = 1,
        ColorId = 1,
        Description = "aile arabası",
        ModelYear = 2022,
        Name = "Hyundai"
    });
}



static void GetCarsByColorId(ICarService carService)
{
    foreach (var car in carService.GetCarsByColorId(1))
    {
        Console.WriteLine("araç id:" + car.Id + " - " + "marka:" + car.BrandId +  " - " + "name:" + car.Name + " - " + "color id:" + car.ColorId + " - " + car.DailyPrice + "TL");
    }
}
