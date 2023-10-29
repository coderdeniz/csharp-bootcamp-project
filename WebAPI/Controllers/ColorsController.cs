using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _colorService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _colorService.GetColorById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Entities.Concrete.Color color)
        {
            return Ok(_colorService.Add(color));
        }

        [HttpPut("update")]
        public IActionResult Update(Entities.Concrete.Color color)
        {
            return Ok(_colorService.Update(color));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Entities.Concrete.Color color)
        {
            return Ok(_colorService.Remove(color));
        }
    }
}
