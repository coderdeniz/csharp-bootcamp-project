using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    BrandId = 1,
                    ColorId = 1,
                    DailyPrice = 1500,
                    ModelYear = 2015,
                    Description = "Aile arabası"
                },
                new Car
                {
                    Id = 2,
                    BrandId = 2,
                    ColorId = 1,
                    DailyPrice = 2500,
                    ModelYear = 2018,
                    Description = "Spor araba"
                }
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car deletedToCar = _cars.SingleOrDefault(c => c.Id == car.Id);

            if (deletedToCar != null)
            {
                _cars.Remove(deletedToCar);
            }
        }

        public Car Get(Expression<Func<Car, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c=> c.Id == id);
        }

        public void Update(Car car)
        {
            Car updatedToCar = _cars.SingleOrDefault(c => c.Id == car.Id);

            if (updatedToCar != null)
            {
                updatedToCar.DailyPrice = car.DailyPrice;
                updatedToCar.ModelYear= car.ModelYear;
                updatedToCar.Description = car.Description;
                updatedToCar.ColorId = car.ColorId;
                updatedToCar.BrandId = car.BrandId;
            }
        }
    }
}
