﻿using System;
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


        public ActionResult DictionaryCountryData([DataSourceRequest]DataSourceRequest request, string CountryName, string CountryCode)//************
        {
            if (string.IsNullOrEmpty(CountryName) == true && string.IsNullOrEmpty(CountryCode) == true)
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

        public ActionResult FliterButton(string CountryCode, string CountryName)
        {
            return Content(CountryName);
        }

        public ActionResult DeleteCounty([DataSourceRequest]DataSourceRequest request, int countyToDeleteId)
        {
            int status = _dictionaryService.DeleteCounty(countyToDeleteId);
            if (status != 500)
                return Json(status);
            else
                return View();
           // return RedirectToAction("Counties");
        }


        public ActionResult DictionarySeasonTypeData([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionarySeasonTypeModels(request.Page, request.PageSize).ToDataSourceResult(request));
        }

        public IActionResult DictionarySeasonType()
        {
            return View();
        }

        public void DeleteCountry([DataSourceRequest] DataSourceRequest request, DictionaryCountry model)
        {
            _dictionaryService.DeleteCountry(model.DictionaryCountryId);                       
        }

        public ActionResult County(int countyToEditId)
        {
            if (countyToEditId == 0)
            {
                return View(new DictionaryCountyModel());

            }
            else
            {
                DictionaryCountyModel countyToEdit = new DictionaryCountyModel();
                countyToEdit = _dictionaryService.getInfoCounty(countyToEditId);
                return View(countyToEdit);
            }
           
        }       

        public ActionResult AddCounty([DataSourceRequest] DataSourceRequest request, string countyName, string countyCode,
            int countryId)
        {
            int status = _dictionaryService.AddCounty(countyName, countyCode, countryId);
            if (status != 500)
                return Json(status);
            else
                return View();
        }

        public ActionResult EditCounty([DataSourceRequest] DataSourceRequest request, int countyId, string countyName, 
            string countyCode, int countryId)
        {
            int status = _dictionaryService.EditCounty(countyId, countyName, countyCode, countryId);
            if (status != 500)
                return Json(status);
            else
                return View();
        }
    }
}