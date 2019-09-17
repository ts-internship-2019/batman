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
            var jsonVar = _dictionaryService.GetDictionaryCity().ToDataSourceResult(request);
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

        public JsonResult GetCascadeCounty()
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
            // List<DictionaryCountyModel> dictionaryCountyModels = _dictionaryService.GetDictionaryCountyModels();

            // return View(dictionaryCountyModels);

            return View();
        }

        public ActionResult CountyData([DataSourceRequest]DataSourceRequest request)
        {
            List<DictionaryCountyModel> data = _dictionaryService.GetDictionaryCountyModels(request.Page, request.PageSize);
            var result = new DataSourceResult()
            {
                Data = data, // process data (paging and sorting applied)
                Total = _dictionaryService.GetDictionaryCountyCount()

            };
            return Json(result);
        }

        public ActionResult FilterCountyData([DataSourceRequest]DataSourceRequest request, string searchCountyName, string searchCountyCode, string searchCountryName)
        {
            return Json(_dictionaryService.FilterDictionaryCountyModels(searchCountyName, searchCountyCode, searchCountryName, request.Page, request.PageSize).ToDataSourceResult(request));
        }

        public ActionResult GetCountryList([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetCountryList());
        }

        public ActionResult ServerFiltering_GetCountries(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetCountries(text));
        }
    }
}