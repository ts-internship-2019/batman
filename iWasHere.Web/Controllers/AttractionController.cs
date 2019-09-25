﻿using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.DTOs;
using System.Collections.Generic;

namespace iWasHere.Web.Controllers
{
    public class AttractionController : Controller
    {
        private readonly AttractionService _attractionService;
        public AttractionController( AttractionService attractionService)
        {
            _attractionService = attractionService;
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

        //public ActionResult AttractionsCountry(int countryId)
        //{
        //    return View();
        //}
        public ActionResult AttractionsCountry(int countryId)
        {
            List<AttractionListModel> attrList = new List<AttractionListModel>();
            attrList = _attractionService.GetAttractionsFromCountry(countryId);
            return View(attrList);

        }
    }
}
