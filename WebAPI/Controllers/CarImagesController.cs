using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;
        private readonly IWebHostEnvironment _webhostEnvironment;

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webhostEnvironment)
        {
            _carImageService = carImageService;
            _webhostEnvironment = webhostEnvironment;
        }

        [HttpPost("AddImage")]
        public IActionResult AddImage([FromForm] CarImageAddDto carImageAddDto)
        {
            var result = _carImageService.Add(carImageAddDto, _webhostEnvironment.WebRootPath);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("DeleteImage")]
        public IActionResult DeleteImage(CarImageDeleteDto carImageDeleteDto)
        {
            var result = _carImageService.Remove(carImageDeleteDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("UpdateImage")]
        public IActionResult UpdateImage([FromForm] CarImageUpdateDto carImageUpdateDto)
        {
            var result = _carImageService.Update(carImageUpdateDto, _webhostEnvironment.WebRootPath);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("ListByCar")]
        public IActionResult ListByCar(int carId)
        {
            var result = _carImageService.ListByCar(carId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
