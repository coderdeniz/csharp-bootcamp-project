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
    public interface ICarImageService 
    {
        IResult Add(CarImageAddDto carImageAddDto, string path);
        IResult Remove(CarImageDeleteDto carImageDeleteDto);
        IResult Update(CarImageUpdateDto carImageUpdateDto, string path);
        IDataResult<List<CarImage>> ListByCar(int carId);
    }
}
