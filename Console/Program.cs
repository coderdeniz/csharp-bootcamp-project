using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

ICarService carService = new CarManager(new EfCarDal());
IColorService colorService = new ColorManager(new EfColorDal());
IBrandService brandService = new BrandManager(new EfBrandDal());
IUserService userService = new UserManager(new EfUserDal());
ICustomerService customerService = new CustomerManager(new EfCustomerDal(),new EfUserDal());
IRentalService rentalService = new RentalManager(new EfRentalDal());


// for user
//var user = new User()
//{
//    FirstName = "Deniz",
//    LastName = "Duman",
//    Email = "denizdumanresmi@gmail.com",
//    PasswordSalt = new byte[64],
//    PasswordHash = new byte[64],
//    Status = true
//};
//Console.WriteLine(userService.AddUser(user).Message);


// for customer
//var customer = new Customer
//{
//    UserId = 1,
//    CompanyName = "DUMAN AŞ."
//};
//Console.WriteLine(customerService.AddCustomer(customer).Message);

// for rental
//var rental = new Rental
//{
//    CarId = 1,
//    CustomerId = 1,
//    RentDate = DateTime.Now.AddDays(-1),
//    ReturnDate = null
//};
//Console.WriteLine(rentalService.AddRental(rental).Message);

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
    foreach (var car in carService.GetCarsByColorId(1).Data)
    {
        Console.WriteLine("araç id:" + car.Id + " - " + "marka:" + car.BrandId + " - " + "name:" + car.Name + " - " + "color id:" + car.ColorId + " - " + car.DailyPrice + "TL");
    }
}

static void GetColor(IColorService colorService)
{
    var car = colorService.GetColorById(1).Data;
    Console.WriteLine(car.Name + " - ");
}

static void GetColors(IColorService colorService)
{
    var colors = colorService.GetAll();
    foreach (var item in colors.Data)
    {
        Console.WriteLine(item.Id + " - " + item.Name + " - ");
    }
}

static void GetCar(ICarService carService)
{
    var car = carService.GetCarById(1).Data;
    Console.WriteLine(car.Name + " - " + car.Description);
}

static void GetCars(ICarService carService)
{
    var cars = carService.GetAll().Data;
    foreach (var item in cars)
    {
        Console.WriteLine(item.Id + " - " + item.Name + " - " + item.Description);
    }
}

static void AddCar(ICarService carService)
{
    var car = new Car
    {
       
    };
    Console.WriteLine(carService.Add(car).Message);
    //GetCars(carService);
}

static void UpdateCar(ICarService carService)
{
    var car = carService.GetCarById(1).Data;
    car.Description += " Update Test";
    carService.Update(car);
    GetCars(carService);
}

static void RemoveCar(ICarService carService)
{
    var car = carService.GetCarById(2).Data;
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
    var color = colorService.GetColorById(1).Data;
    color.Name += "update";
    colorService.Update(color);
    GetColors(colorService);
}

static void RemoveColor(IColorService colorService)
{
    var color = colorService.GetColorById(1).Data;
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
    foreach (var item in brandService.GetAll().Data)
    {
        Console.WriteLine("{0} - {1}", item.Id, item.Name);
    }
}

static void GetBrand(IBrandService brandService)
{
    Console.WriteLine(brandService.GetBrandById(4).Data.Name);
}

static void UpdateBrand(IBrandService brandService)
{
    var brand = brandService.GetBrandById(1).Data;
    brand.Name += " update";
    brandService.Update(brand);
    GetBrands(brandService);
}

static void RemoveBrand(IBrandService brandService)
{
    brandService.Remove(brandService.GetBrandById(4).Data);
    GetBrands(brandService);
}

static void GetCarDetails(ICarService carService)
{
    foreach (var car in carService.GetCarDetails().Data)
    {
        Console.WriteLine($"araba: {car.CarName} - renk: {car.ColorName} - marka: {car.BrandName} - günlük ücret: {car.DailyPrice}TL ");
    }
}