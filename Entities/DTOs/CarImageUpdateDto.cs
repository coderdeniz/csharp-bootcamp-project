using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs
{
    public class CarImageUpdateDto : IDto, IFile
    {
        public int Id { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
