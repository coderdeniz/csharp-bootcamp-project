﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _brandService.GetBrandById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Brand brand)
        {
            return Ok(_brandService.Add(brand));
        }

        [HttpPut("update")]
        public IActionResult Update(Brand brand)
        {
            return Ok(_brandService.Update(brand));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Brand brand)
        {
            return Ok(_brandService.Remove(brand));
        }
    }
}
