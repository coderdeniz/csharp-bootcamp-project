﻿using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<IList<Customer>> GetAllCustomers();
        IResult AddCustomer(Customer customer);
        IResult UpdateCustomer(Customer customer);
        IResult DeleteCustomer(Customer customer);
        IDataResult<Customer> GetCustomerById(int customerId);
    }
}
