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

        public IActionResult Currency()
        {
            
            return View();

        }

        public ActionResult CurrencyAdd()
        {
            return View();
        }

        public ActionResult CurrencyData([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCurrencyTypeModels(request.Page, request.PageSize).ToDataSourceResult(request));

        }

     


    }
}