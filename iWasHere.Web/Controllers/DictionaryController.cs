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



        public IActionResult Index()
        {
            return Json(_dictionaryService.GetCountryList());
        }

        public ActionResult ServerFiltering_GetCountries(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetCountries(text));
        }

        public IActionResult Currency()
        {
            
            return View();

        }

        public IActionResult CurrencyAdd(int id)
        {
            if(id!=0)
            {
                DictionaryCurrencyType dict = _dictionaryService.CurrencyEdit(id);
                return View(dict);
            }
           else
          
            {
                DictionaryCurrencyType dict = new DictionaryCurrencyType();
                return View(dict);

            }

            
        }

        public ActionResult CurrencyData([DataSourceRequest]DataSourceRequest request, string currencyName)
        {
            List<DictionaryCurrencyType> data = _dictionaryService.GetDictionaryCurrencyTypeModels(request.Page, request.PageSize, currencyName );
             var result = new DataSourceResult()
            {
                Data = data, 
                 Total = _dictionaryService.GetDictionaryCurrencyTypeModels1(request.Page, request.PageSize, currencyName)
             };
           
             return Json(result);
        }

      

       

    

        public ActionResult DeleteCurrency([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != 0)
            {
                _dictionaryService.DeleteCurrency(id);
            }
            return Json(ModelState.ToDataSourceResult());
        }


        public ActionResult EditCurrency(int typeId, string currencyName, string currencyCode)
        {
            

            DictionaryCurrencyType dictionary = new DictionaryCurrencyType();
        
            dictionary.DictionaryCurrencyTypeId = typeId;
            dictionary.DictionaryCurrencyName = currencyName;
            dictionary.DictionaryCurrencyCode = currencyCode;


            DatabaseContext database = new DatabaseContext();

            
            database.DictionaryCurrencyType.Update(dictionary);


            return Json(database.SaveChanges());


        }

      




        public ActionResult CurrencySave( string currencyName, string currencyCode)
        {
            DatabaseContext database = new DatabaseContext();
            database.DictionaryCurrencyType.Add(new DictionaryCurrencyType
            {
                
                DictionaryCurrencyName = currencyName,
                DictionaryCurrencyCode = currencyCode

            });

            return Json(database.SaveChanges());

        }




    }
}