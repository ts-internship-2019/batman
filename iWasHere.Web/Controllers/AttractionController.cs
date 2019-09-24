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

