using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace iWasHere.Web.Controllers
{

    public class PhotoController : Controller
    {
        private readonly PhotoService _photoService;
        public PhotoController(PhotoService photoService)
        {
            _photoService = photoService;
        }

        public ActionResult GetAttractionsForCombo()
        {
            return Json(_photoService.GetAttractions());
        }

        public IActionResult AddPhoto()
        {
            return View();
        }
        public IActionResult ClientFiltering()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(int attractionId, IEnumerable<IFormFile> files )
        {
            int status = 0;
            //attractionId = 14;
            IEnumerable<string> fileInfo = new List<string>();

            if (files != null)
            {
                fileInfo = GetFileInfo(files,attractionId);
            }
            return Json(status);
        }

        
        public ActionResult Result()
        {
            return View();
        }

        private IEnumerable<string> GetFileInfo(IEnumerable<IFormFile> files, int attractionId)
        {
            int status = 0;
            List<string> fileInfo = new List<string>();

            foreach (var file in files)
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                var filePath = Path.GetFullPath(fileContent.FileName.ToString().Trim('"'));

                status = _photoService.AddPhoto(attractionId, fileName, filePath);
                fileInfo.Add(string.Format("{0} ({1} bytes)", fileName, file.Length,filePath));
            }

            return fileInfo;
        }
    }
}
