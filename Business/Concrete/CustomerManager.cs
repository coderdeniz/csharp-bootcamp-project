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
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IUserDal _userDal;

        public CustomerManager(ICustomerDal customerDal, IUserDal userDal)
        {
            _customerDal = customerDal;
            _userDal = userDal;
        }

        public IResult AddCustomer(Customer customer)
        {
            var user = _userDal.Get(u => u.Id == customer.UserId);
            if (user == null)
            {
                return new ErrorResult(Messages.AddedCustomerErrorNotFoundUser);
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.AddedCustomer);
        }

        public IResult DeleteCustomer(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IDataResult<IList<Customer>> GetAllCustomers()
        {
            var customers = _customerDal.GetAll();
            return new SuccessDataResult<IList<Customer>>(customers);
        }

        public IDataResult<Customer> GetCustomerById(int customerId)
        {
            var customer = _customerDal.Get(c => c.UserId == customerId);
            return new SuccessDataResult<Customer>(customer);
        }

        public IResult UpdateCustomer(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult();
        }
    }
}
