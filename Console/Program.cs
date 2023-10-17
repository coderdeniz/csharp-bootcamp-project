using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

ICarService carService = new CarManager(new EfCarDal());
IColorService colorService = new ColorManager(new EfColorDal());
IBrandService brandService = new BrandManager(new EfBrandDal());


// for car

//GetCars(carService);
//GetCar(carService);
//AddCar(carService);
//UpdateCar(carService);
//RemoveCar(carService);
//GetCarDetails(carService);


// for color

//GetColors(colorService);
//GetColor(colorService);
//AddColor(colorService);
//UpdateColor(colorService);
//RemoveColor(colorService);


// for brand

//GetBrands(brandService);
//GetBrand(brandService);
//AddBrand(brandService);
//UpdateBrand(brandService);
//RemoveBrand(brandService);






//Add(carService);
//GetCarsByColorId(carService);






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
        Console.WriteLine("araç id:" + car.Id + " - " + "marka:" + car.BrandId + " - " + "name:" + car.Name + " - " + "color id:" + car.ColorId + " - " + car.DailyPrice + "TL");
    }
}

static void GetColor(IColorService colorService)
{
    var car = colorService.GetColorById(1);
    Console.WriteLine(car.Name + " - ");
}

static void GetColors(IColorService colorService)
{
    var colors = colorService.GetAll();
    foreach (var item in colors)
    {
        Console.WriteLine(item.Id + " - " + item.Name + " - ");
    }
}

static void GetCar(ICarService carService)
{
    var car = carService.GetCarById(1);
    Console.WriteLine(car.Name + " - " + car.Description);
}

static void GetCars(ICarService carService)
{
    var cars = carService.GetAll();
    foreach (var item in cars)
    {
        Console.WriteLine(item.Id + " - " + item.Name + " - " + item.Description);
    }
}

static void AddCar(ICarService carService)
{
    var car = new Car
    {
        BrandId = 1,
        ColorId = 3,
        Description = "2 kişilik",
        Name = "Opel Corsa",
        DailyPrice = 760,
        ModelYear = 2011
    };
    carService.Add(car);
    GetCars(carService);
}

static void UpdateCar(ICarService carService)
{
    var car = carService.GetCarById(1);
    car.Description += " Update Test";
    carService.Update(car);
    GetCars(carService);
}

static void RemoveCar(ICarService carService)
{
    var car = carService.GetCarById(2);
    carService.Remove(car);
    GetCars(carService);
}

static void AddColor(IColorService colorService)
{
    var color = new Color
    {
        Name = "Kırmızı"
    };
    colorService.Add(color);
    GetColors(colorService);
}

static void UpdateColor(IColorService colorService)
{
    var color = colorService.GetColorById(1);
    color.Name += "update";
    colorService.Update(color);
    GetColors(colorService);
}

static void RemoveColor(IColorService colorService)
{
    var color = colorService.GetColorById(1);
    colorService.Remove(color);
    GetColors(colorService);
}

static void AddBrand(IBrandService brandService)
{
    var brand = new Brand
    {
        Name = "Bmw"
    };
    brandService.Add(brand);
    brand = new Brand();
    brand.Name = "Hyundai";
    brandService.Add(brand);
}

static void GetBrands(IBrandService brandService)
{
    foreach (var item in brandService.GetAll())
    {
        Console.WriteLine("{0} - {1}", item.Id, item.Name);
    }
}

static void GetBrand(IBrandService brandService)
{
    Console.WriteLine(brandService.GetBrandById(4).Name);
}

static void UpdateBrand(IBrandService brandService)
{
    var brand = brandService.GetBrandById(1);
    brand.Name += " update";
    brandService.Update(brand);
    GetBrands(brandService);
}

static void RemoveBrand(IBrandService brandService)
{
    brandService.Remove(brandService.GetBrandById(4));
    GetBrands(brandService);
}

static void GetCarDetails(ICarService carService)
{
    foreach (var car in carService.GetCarDetails())
    {
        Console.WriteLine($"araba: {car.CarName} - renk: {car.ColorName} - marka: {car.BrandName} - günlük ücret: {car.DailyPrice}TL ");
    }
}