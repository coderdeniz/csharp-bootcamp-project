using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public void AddImage(CarImage carImage)
        {
            using ReCapProjectContext context = new ReCapProjectContext();

            var addedEntity = context.Entry(carImage);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public CarImage? DeleteImage(int carImageId)
        {
            using ReCapProjectContext context = new ReCapProjectContext();
            var entity = context.Set<CarImage>().SingleOrDefault(c => c.Id == carImageId);
            if (entity != null)
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
                return entity;
            }
            return entity;
        }

        public List<CarDto> GetCarDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new CarDto
                             {
                                 BrandName = b.Name,
                                 CarName = c.Name,
                                 ColorName = co.Name,
                                 DailyPrice = c.DailyPrice
                             };

                return result.ToList();
            }
        }
    }
}
