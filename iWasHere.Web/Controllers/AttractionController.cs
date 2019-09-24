using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

namespace iWasHere.Web.Controllers
{
    public class AttractionController : Controller
    {
        private readonly AttractionService _attractionService;
        private readonly DatabaseContext _dbContext;

        public AttractionController(AttractionService attractionService, DatabaseContext databaseContext)
        {
            _attractionService = attractionService;
            _dbContext = databaseContext;
        }       

        public ActionResult Attraction(int attrId)
        {
            AttractionModel attr = new AttractionModel();
            attr = _attractionService.GetAttractionLocation(attrId);
            return View(attr);
        }
    }
}
