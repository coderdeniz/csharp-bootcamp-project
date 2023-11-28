using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Files
{
    public class FileHelper
    {
        public static IDataResult<string> AddFile(IFormFile file,string path,out string fullPath)
        {
            fullPath = string.Empty;

            List<string> extensionList = new List<string> { ".png", ".jpg", ".jpeg" };

            string fileExtension = Path.GetExtension(file.FileName);

            if (!extensionList.Contains(fileExtension))
            {
                return new ErrorDataResult<string>(null,"Yanlış format gönderimi");
            }

            string folderRoad = path;

            if (!Directory.Exists(folderRoad))
            {
                Directory.CreateDirectory(folderRoad);
            }

            string fileGuid = Guid.NewGuid().ToString();

            string fileName = fileGuid + fileExtension;

            string fileRoad = Path.Combine(folderRoad, fileName);

            using (var fileStream = new FileStream(fileRoad, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            fullPath = fileRoad;

            return new SuccessDataResult<string>("Dosya başarıyla eklendi");
        }
        public static IDataResult<string> RemoveFile(string path)
        {

            string folderRoad = path;

            if (!File.Exists(folderRoad))
            {
                return new ErrorDataResult<string>(null, "Belirtilen dosya sistemde yok");
            }

            try
            {
                File.Delete(folderRoad);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<string>(null, e.Message);
            }

            return new SuccessDataResult<string>(folderRoad, "Dosya başarıyla kaldırıldı");
        }

    }
}
