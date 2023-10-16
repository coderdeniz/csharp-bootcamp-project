﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        void Add(Brand brand);
        void Remove(Brand brand);
        void Update(Brand brand);
        Brand GetBrandById(int brandId);
    }
}
