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

        public IActionResult Landmark(DictionaryLandmarkTypeModel dictionary)
        {
            DictionaryLandmarkType dictionaryLandmarkType = _dictionaryService.GetSelectedLandmark(dictionary.Id);
            return View(dictionaryLandmarkType);
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
        public ActionResult DictionaryLandmarkData([DataSourceRequest]DataSourceRequest request, int? landmarkId, string LandmarkName,string LandmarkCode)
        {
            List<DictionaryLandmarkTypeModel> data = _dictionaryService.GetDictionaryLandmarkModels(request.Page, request.PageSize,
                 landmarkId, LandmarkName, LandmarkCode, out int landmarkCount);
            //List<DictionaryLandmarkTypeModel> data = _dictionaryService.GetDictionaryLandmarkTypeModels(request.Page, request.PageSize);
            var result = new DataSourceResult()
            {
                Data = data, 
                Total = landmarkCount

            };
            return Json(result);
        }
        public ActionResult InsertLandmark(DictionaryLandmarkType landmarkType)
        {
            string status = "";
            _dictionaryService.InsertLandmark(landmarkType);

            return Json(status);
        }
        
        public IActionResult UpdateLandmark(DictionaryLandmarkType landmarkType)
        {
            string status = "";
            _dictionaryService.UpdateLandmark(landmarkType);

            return Json(status);
        }
        public ActionResult LandmarkForm(DictionaryLandmarkType landmarkType)
        {
            if (landmarkType.DictionaryItemId<1)
            {
                _dictionaryService.InsertLandmark(landmarkType);
            }
            else
            {
                _dictionaryService.UpdateLandmark(landmarkType);
            }
            return View(landmarkType);
        }
        public IActionResult Counties()
        {
            return View();
        }
        //public ActionResult SaveLandmark()
        //{

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
        public ActionResult ServerFiltering_GetLandmarks(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetLandmarks(text));
        }

        public IActionResult County()
        {
            return View();
        }

        public ActionResult FliterButton(string CountryCode, string CountryName)
        { 
            return Content(CountryName);
        }
        public ActionResult DeleteCounty([DataSourceRequest]DataSourceRequest request, int countyId)
        {
            _dictionaryService.DeleteCounty(countyId);

            return RedirectToAction("Counties");
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
        public void DeleteLandmark([DataSourceRequest] DataSourceRequest request, int landmarkToDelete)
        {
            _dictionaryService.DeleteCountry(landmarkToDelete);


        }
    }
}