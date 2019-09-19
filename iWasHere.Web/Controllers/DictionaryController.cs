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


        public object SeasonName { get; private set; }

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
            var jsonVar = _dictionaryService.GetDictionaryCity(request.Page,request.PageSize).ToDataSourceResult(request);
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


        public ActionResult DictionaryCountryData([DataSourceRequest]DataSourceRequest request, string CountryName, string CountryCode)//************
        {
            if(string.IsNullOrEmpty(CountryName) == true && string.IsNullOrEmpty(CountryCode) == true)
            {
                var jsonVar = _dictionaryService.GetDictionaryCountry(request.Page, request.PageSize);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetCountryCount() // Total number of records
                };
                return Json(result);
            }
            else if (string.IsNullOrEmpty(CountryName) == false && string.IsNullOrEmpty(CountryCode) == true)
                {
                    var jsonVar = _dictionaryService.FilterCountriesByName(request.Page, request.PageSize, CountryName);
                    DataSourceResult result = new DataSourceResult()
                    {
                        Data = jsonVar, // Process data (paging and sorting applied)
                        Total = _dictionaryService.GetCountryCount() // Total number of records
                    };
                    return Json(result);
                }
                else if (string.IsNullOrEmpty(CountryName) == true && string.IsNullOrEmpty(CountryCode) == false)
                {
                    var jsonVar = _dictionaryService.FilterCountriesByCode(request.Page, request.PageSize, CountryCode);
                    DataSourceResult result = new DataSourceResult()
                    {
                        Data = jsonVar, // Process data (paging and sorting applied)
                        Total = _dictionaryService.GetCountryCount() // Total number of records
                    };
                    return Json(result);
                }
                else
                {
                        var jsonVar = _dictionaryService.FilterCountriesByCodeAndName(request.Page, request.PageSize, CountryName, CountryCode);
                        DataSourceResult result = new DataSourceResult()
                        {
                            Data = jsonVar, // Process data (paging and sorting applied)
                            Total = _dictionaryService.GetCountryCount() // Total number of records
                        };
                        return Json(result);
                }
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

        public ActionResult FliterButton(string CountryCode, string CountryName)
        { 
            return Content(CountryName);
        }
        public ActionResult ServerFiltering_GetCountries(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetCountries(text));
        }


        public ActionResult DictionarySeasonTypeData([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionarySeasonTypeModels(request.Page, request.PageSize).ToDataSourceResult(request));
        }

        public IActionResult DictionarySeasonType()
        {
            return View();
        }
    }
}