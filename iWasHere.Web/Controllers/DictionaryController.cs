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

        public IActionResult Index()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View(dictionaryLandmarkTypeModels);
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