using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.DTOs;


namespace iWasHere.Web.Controllers
{
    public class AttractionController : Controller
    {
        private readonly AtractionService _attractionService;
        public AttractionController(AtractionService atractionService)
        {
            _attractionService = atractionService;
        }

        public ActionResult Attraction(int attrId)
        {
            AttractionModel attr = new AttractionModel();
            attr = _attractionService.GetAttractionLocation(attrId);
            return View(attr);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAttractions([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_attractionService.GetAttractionListModels().ToDataSourceResult(request));
        }
    }
}
