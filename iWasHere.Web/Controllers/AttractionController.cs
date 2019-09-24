using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class AttractionController : Controller
    {
        private readonly AtractionService _atractionService;
        public AttractionController(AtractionService atractionService)
        {
            _atractionService = atractionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAttractions([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_atractionService.GetAttractionListModels().ToDataSourceResult(request));
        }
    }
}