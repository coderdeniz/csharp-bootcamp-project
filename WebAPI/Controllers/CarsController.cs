using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IWebHostEnvironment _webhostEnvironment;

        public CarsController(ICarService carService, IWebHostEnvironment webhostEnvironment)
        {
            _carService = carService;
            _webhostEnvironment = webhostEnvironment;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetCarById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            return Ok(_carService.Add(car));
        }

        [HttpPut("update")]
        public IActionResult Update(Car car)
        {
            return Ok(_carService.Update(car));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Car car)
        {
            return Ok(_carService.Remove(car));
        }

        [HttpGet("getcardetails")]
        public IActionResult GetCarDetails()
        {
            var result = _carService.GetCarDetails();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcarsbybrandid")]
        public IActionResult GetCarsByBrandId(int brandId)
        {
            var result = _carService.GetCarsByBrandId(brandId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcarsbycolorid")]
        public IActionResult GetCarsByColorId(int colorId)
        {
            var result = _carService.GetCarsByColorId(colorId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("AddImage")]
        public IActionResult AddImage([FromForm] CarImageAddDto carImageAddDto)
        {
            var result = _carService.AddImage(carImageAddDto,_webhostEnvironment.WebRootPath);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("DeleteImage")]
        public IActionResult AddImage(CarImageDeleteDto carImageDeleteDto)
        {
            var result = _carService.RemoveImage(carImageDeleteDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
