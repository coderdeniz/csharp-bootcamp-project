using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IResult Add(Car car);
        IResult Remove(Car car);
        IResult Update(Car car);
        IDataResult<Car> GetCarById(int carId);
        IDataResult<List<CarDto>> GetCarDetails();
        IResult AddImage(CarImageAddDto carImageAddDto, string path);
        IResult RemoveImage(CarImageDeleteDto carImageDeleteDto);
    }
}
