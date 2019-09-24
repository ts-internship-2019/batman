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
        private readonly AtractionService _atractionService;
        private readonly DatabaseContext _dbContext;

        public AtractionController(AtractionService atractionService, DatabaseContext databaseContext)
        {
            _atractionService = atractionService;
            _dbContext = databaseContext;
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
            return Json(_atractionService.ServerFiltering_GetCountries(text));
        }
        public ActionResult ServerFiltering_GetCounties(int? countryId,string text)
        {
            return Json(_atractionService.ServerFiltering_GetCounties(countryId, text));
        }
        public ActionResult ServerFiltering_GetCities(int ? countyId ,string text)
        {
            return Json(_atractionService.ServerFiltering_GetCities(countyId, text));
        }
        public ActionResult ServerFiltering_GetLandmarks(string text)
        {
            return Json(_atractionService.ServerFiltering_GetLandmarks(text));
        }
        public ActionResult ServerFiltering_GetSeasons(string text)
        {
            return Json(_atractionService.ServerFiltering_GetSeasons(text));
        }
    }
}