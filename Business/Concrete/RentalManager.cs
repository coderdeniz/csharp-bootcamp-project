using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult AddRental(Rental rental)
        {
            var isRented = _rentalDal.Get(r => r.ReturnDate == null && r.CarId == rental.CarId);

            if (isRented != null)
            {
                return new ErrorResult(Messages.AddedRentalErrorRentedCar);
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.AddedRental);
        }

        public IResult DeleteRental(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<IList<Rental>> GetAllRentals()
        {
            var rentalList = _rentalDal.GetAll();
            return new SuccessDataResult<IList<Rental>>(rentalList);
        }

        public IDataResult<Rental> GetRentalById(int rentalId)
        {
            var rental = _rentalDal.Get(r => r.Id == rentalId);
            return new SuccessDataResult<Rental>(rental);
        }

        public IResult UpdateRental(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }
    }
}
