using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Files;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImageAddDto carImageAddDto, string path)
        {
            string folderRoad = Path.Combine(path, "cars");

            var result = BusinessRules.Run(AddCarImageToFolder(carImageAddDto.ImageFile, folderRoad, out string fullPath),
            CountLimitOfCarImages(carImageAddDto.CarId));

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

            _carImageDal.Add(carImage);

            return new SuccessResult();
        }

        public IResult Remove(CarImageDeleteDto carImageDeleteDto)
        {
            var carImage = _carImageDal.Get(c=>c.Id == carImageDeleteDto.Id);
              
            if (carImage == null)
            {
                return new ErrorResult();
            }

            _carImageDal.Delete(carImage);
            
            var result = FileHelper.RemoveFile(carImage.ImagePath);

            return result;
        }

        public IResult Update(CarImageUpdateDto carImageUpdateDto, string path)
        {
            var carImage = _carImageDal.Get(c => c.Id == carImageUpdateDto.Id);
            string folderRoad = Path.Combine(path, "cars");

            if (carImage == null)
            {
                return new ErrorResult();
            }

            var result = BusinessRules.Run(FileHelper.RemoveFile(carImage.ImagePath), AddCarImageToFolder(carImageUpdateDto.ImageFile, folderRoad, out string fullPath));

            if (!result.Success)
            {
                return new ErrorResult();
            }           

            carImage.ImagePath = fullPath;
            carImage.Date = DateTime.Now;

            _carImageDal.Update(carImage);

            return new SuccessResult();
        }

        private IResult CountLimitOfCarImages(int id)
        {
            var countImage = _carImageDal.GetAll(c => c.CarId == id).Count;

            if (countImage >= 5)
            {
                return new ErrorResult("Bir araç için 5'ten fazla resim eklenemez.");
            }

            return new SuccessResult();
        }

        private IResult AddCarImageToFolder(IFormFile imageFile, string folderRoad, out string fullPath)
        {
            var result = FileHelper.AddFile(imageFile, folderRoad,out fullPath);            
            return result;           
        }

        public IDataResult<List<CarImage>> ListByCar(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);

            if (result.Count == 0)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage>() { new CarImage
                {
                    CarId = carId,
                    Date = DateTime.Now,
                    Id = 1,
                    ImagePath = "defaultPath"
                }});
            }

            return new SuccessDataResult<List<CarImage>>(result);
        }
    }
}
