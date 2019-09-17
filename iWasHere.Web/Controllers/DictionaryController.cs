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

        public IActionResult Index()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View(dictionaryLandmarkTypeModels);
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