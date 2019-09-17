using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public IActionResult DictionaryLandmark()
        {
            //List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View();
        }

        public IActionResult City()
        {
            return View();
        }


        public ActionResult CityData([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCity(request.Page, request.PageSize).ToDataSourceResult(request));
        }

        public IActionResult DictionaryCountry()
        {
            return View();
        }


        public ActionResult DictionaryCountryData([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCountry(request.Page, request.PageSize).ToDataSourceResult(request));
        }
        public ActionResult DictionaryLandmarkData([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryLandmarkTypeModels(request.Page, request.PageSize).ToDataSourceResult(request));
        }
    }
}