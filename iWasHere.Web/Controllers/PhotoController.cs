using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;
using iWasHere.Domain.DTOs;
using System.Collections.Generic;
using Spire.Pdf.General.Paper.Font.Common.Environment;

namespace iWasHere.Web.Controllers
{

    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly PhotoService _photoService;
        public PhotoController(PhotoService photoService, IHostingEnvironment hostingEnvironment)
        {
            _photoService = photoService;
            _hostingEnvironment = hostingEnvironment;
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
        public void Submit(List<IFormFile> files, int LandmarkId)
        {
            List<string> path = new List<string>();
            foreach (var image in files)
            {
                LandmarkId = 14;
                if (image.Length > 0)
                {
                    //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                    var a = Guid.NewGuid().ToString();
                    var fileName = Path.Combine(_hostingEnvironment.WebRootPath + "/images", a + Path.GetExtension(image.FileName));
                    image.CopyTo(new FileStream(fileName, FileMode.Create));
                    path.Add(a + Path.GetExtension(image.FileName));
                }
            }
            foreach (string p in path)
            {
                _photoService.SaveImagesDB(p, LandmarkId);
            }
        }
    }
}