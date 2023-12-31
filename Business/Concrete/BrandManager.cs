﻿using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }


        //[SecuredOperation("brand.add,admin")]
        //[ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.AddedBrand);
        }
        
        [CacheAspect(3)]
        public IDataResult<List<Brand>> GetAll()
        {
            var result = _brandDal.GetAll();

            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.NoDataOnList);
        }

        public IDataResult<Brand> GetBrandById(int brandId)
        {
            var result = _brandDal.Get(b => b.Id == brandId);

            if (result != null)
            {
                return new SuccessDataResult<Brand>(result);
            }
            return new SuccessDataResult<Brand>(result,Messages.NoDataOnFilter);
        }

        public IResult Remove(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult();
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult();
        }
    }
}
