using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
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
        public IActionResult Cities()
        {
            return View();
        }



        public ActionResult CitiesData([DataSourceRequest]DataSourceRequest request)
        {
            var jsonVar = _dictionaryService.GetDictionaryCity(request.Page, request.PageSize).ToDataSourceResult(request);
            //jsonVar.Total = 23;
            return Json(jsonVar);

        }
        public IActionResult City()
        {
            return View();
        }
        public IActionResult ClientFiltering()
        {
            return View();
        }

        public JsonResult GetCascadeCounty([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCity(request.Page, request.PageSize).ToDataSourceResult(request));
        }

        public IActionResult DictionaryCountry()
        {
            return View();
        }


        public ActionResult DictionaryCountryData([DataSourceRequest]DataSourceRequest request)
        {
            List<DictionaryCountry> data = _dictionaryService.GetDictionaryCountry(request.Page, request.PageSize);
            //int total = data.Count();

            var result = new DataSourceResult()
            {
                Data = data, // Process data (paging and sorting applied)
                Total = _dictionaryService.GetCountryCount() // Total number of records
            };
            return Json(result);
        }
        public ActionResult DictionaryLandmarkData([DataSourceRequest]DataSourceRequest request)
        {
            List<DictionaryLandmarkTypeModel> data = _dictionaryService.GetDictionaryLandmarkTypeModels(request.Page, request.PageSize);
            //int total = data.Count();
            var result = new DataSourceResult()
            {
                Data = data, // Process data (paging and sorting applied)
                Total = _dictionaryService.GetLandmarkCount() // Total number of records
            };
            return Json(result);
        }

        public IActionResult Counties()
        {
            return View();
        }

        public ActionResult CountyData([DataSourceRequest]DataSourceRequest request,
            int? countryId, string countyName, string countyCode)
        {
            List<DictionaryCountyModel> data = _dictionaryService.GetDictionaryCountyModels(request.Page, request.PageSize,
                 countryId, countyName, countyCode, out int countiesCount);
            var result = new DataSourceResult()
            {
                Data = data, 
                Total = countiesCount              

            };
            return Json(result);
        }       

        public ActionResult ServerFiltering_GetCountries(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetCountries(text));
        }

        public IActionResult County()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCounty([DataSourceRequest]DataSourceRequest request, int countyId)
        {
            _dictionaryService.DeleteCounty(countyId);

            return RedirectToAction("Counties");
        }
    }
}