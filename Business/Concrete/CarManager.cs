using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Files;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.Name?.Length >= 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.AddedCar);
            }
            else
            {
                return new ErrorResult(Messages.CarNameOrPriceError);
            }
        }

        public IResult AddImage(CarImageAddDto carImageAddDto, string path)
        {
            string folderRoad = Path.Combine(path, "cars");

            var result = FileHelper.AddFile(carImageAddDto.ImageFile,folderRoad, out string fullPath);

            if (!result.Success)
            {
                return result;
            }

            CarImage carImage = new CarImage
            {
                CarId = carImageAddDto.CarId,
                Date = DateTime.Now,
                ImagePath = fullPath
            };

            _carDal.AddImage(carImage);

            return result;        
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<Car> GetCarById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c=>c.Id == carId));
        }

        public IDataResult<List<CarDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {            
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IResult Remove(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IResult RemoveImage(CarImageDeleteDto carImageDeleteDto)
        {
            var entity = _carDal.DeleteImage(carImageDeleteDto.Id);
            if (entity == null)
            {
                return new ErrorResult("Veri tabanında ilgili dosya bulunamadı");
            }

            var result = FileHelper.RemoveFile(entity.ImagePath);

            return result;
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }
    }
}
