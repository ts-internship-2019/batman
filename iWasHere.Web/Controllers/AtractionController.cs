using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Service;
using iWasHere.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
namespace iWasHere.Web.Controllers
{
    public class AtractionController : Controller
    {
        private readonly AttractionService _attractionService;

        public AtractionController(AttractionService attractionService, DatabaseContext databaseContext)
        {
            _attractionService = attractionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddOrEditAtraction()
        {

            var atraction = new Attractions();

            return View(atraction);
        }
        public ActionResult ServerFiltering_GetCountries(string text)
        {
            return Json(_attractionService.ServerFiltering_GetCountries(text));
        }
        public ActionResult ServerFiltering_GetCounties(int? countryId,string text)
        {
            return Json(_attractionService.ServerFiltering_GetCounties(countryId, text));
        }
        public ActionResult ServerFiltering_GetCities(int ? countyId ,string text)
        {
            return Json(_attractionService.ServerFiltering_GetCities(countyId, text));
        }
        public ActionResult ServerFiltering_GetLandmarks(string text)
        {
            return Json(_attractionService.ServerFiltering_GetLandmarks(text));
        }
        public ActionResult ServerFiltering_GetSeasons(string text)
        {
            return Json(_attractionService.ServerFiltering_GetSeasons(text));
        }
    }
}