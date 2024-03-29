﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private readonly DatabaseContext _dbContext;

        public DictionaryController(DictionaryService dictionaryService, DatabaseContext databaseContext)
        {
            
            _dictionaryService = dictionaryService;
            _dbContext = databaseContext;
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



        public IActionResult Landmark(DictionaryLandmarkTypeModel dictionary)
        {
            DictionaryLandmarkType dictionaryLandmarkType = _dictionaryService.GetSelectedLandmark(dictionary.Id);
            return View(dictionaryLandmarkType);
        }

        public IActionResult ClientFiltering()
        {
            return View();
        }



        public IActionResult DictionaryCountry()
        {
            return View();
        }




        public ActionResult DictionaryCountryData([DataSourceRequest]DataSourceRequest request, string CountryName, string CountryCode)//************
        {
            if (string.IsNullOrEmpty(CountryName) == true && string.IsNullOrEmpty(CountryCode) == true)
            {
                var jsonVar = _dictionaryService.GetDictionaryCountry(request.Page, request.PageSize);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetCountryCount() // Total number of records
                };
                return Json(result);
            }
            else if (string.IsNullOrEmpty(CountryName) == false && string.IsNullOrEmpty(CountryCode) == true)
            {
                var jsonVar = _dictionaryService.FilterCountriesByName(request.Page, request.PageSize, CountryName);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetCountryCount() // Total number of records
                };
                return Json(result);
            }
            else if (string.IsNullOrEmpty(CountryName) == true && string.IsNullOrEmpty(CountryCode) == false)
            {
                var jsonVar = _dictionaryService.FilterCountriesByCode(request.Page, request.PageSize, CountryCode);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetCountryCount() // Total number of records
                };
                return Json(result);
            }
            else
            {
                var jsonVar = _dictionaryService.FilterCountriesByCodeAndName(request.Page, request.PageSize, CountryName, CountryCode);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetCountryCount() // Total number of records
                };
                return Json(result);
            }
        }
        public ActionResult DictionaryLandmarkData([DataSourceRequest]DataSourceRequest request, int? landmarkId, string LandmarkName, string LandmarkCode)
        {
            List<DictionaryLandmarkTypeModel> data = _dictionaryService.GetDictionaryLandmarkModels(request.Page, request.PageSize,
                 landmarkId, LandmarkName, LandmarkCode, out int landmarkCount);
            //List<DictionaryLandmarkTypeModel> data = _dictionaryService.GetDictionaryLandmarkTypeModels(request.Page, request.PageSize);
            var result = new DataSourceResult()
            {
                Data = data,
                Total = landmarkCount

            };
            return Json(result);
        }
        public ActionResult InsertLandmark(DictionaryLandmarkType landmarkType)
        {
            string status = "";
            _dictionaryService.InsertLandmark(landmarkType,out string errorMessage);

            return Json(status);
        }

        public IActionResult UpdateLandmark(DictionaryLandmarkType landmarkType)
        {
            string status = "";
            _dictionaryService.UpdateLandmark(landmarkType, out string errorMessage);

            return Json(status);
        }
        public ActionResult LandmarkForm(DictionaryLandmarkType landmarkType)
        {

            var errorMessage = "";
            var errorMessage2 = "";
            if (landmarkType.DictionaryItemId < 1)
            {
                _dictionaryService.InsertLandmark(landmarkType, out  errorMessage);
            }
            else
            {
                _dictionaryService.UpdateLandmark(landmarkType, out  errorMessage2);
            }

            ModelState.AddModelError("", errorMessage);
            ModelState.AddModelError("", errorMessage2);
            return View(landmarkType);
        }
        public IActionResult Counties()
        {
            return View();
        }

        public ActionResult CountyData([DataSourceRequest]DataSourceRequest request,
            int? countryId, string countyName, string countyCode)
        {
            List<DictionaryCountyModel> data = _dictionaryService.GetDictionaryCountyModels(request.Page, request.PageSize,
                 countryId, countyName, countyCode, out int countiesCount);
            var result = new DataSourceResult()
            {
                Data = data,
                Total = countiesCount

            };
            return Json(result);
        }

        public ActionResult CitiesData([DataSourceRequest]DataSourceRequest request, int countyId, string cityName)
        {
            List<DictionaryCityModel> data = _dictionaryService.GetDictionaryCity(request.Page, request.PageSize,
                 countyId, cityName).Item1;
            var result = new DataSourceResult()
            {
                Data = data,
                Total = _dictionaryService.GetDictionaryCity(request.Page, request.PageSize, countyId, cityName).Item2
            };
            return Json(result);
        }

        public ActionResult ServerFiltering_GetLandmarks(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetLandmarks(text));
        }
        public ActionResult ServerFiltering_GetSeasons(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetSeasons(text));
        }

        public ActionResult ServerFiltering_GetCountries(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetCountries(text));
        }
        public ActionResult FliterButton(string CountryCode, string CountryName)
        {
            return Content(CountryName);
        }


        public IActionResult Currency()
        {
            return View();
        }


        public ActionResult DeleteCounty([DataSourceRequest]DataSourceRequest request, int countyToDeleteId)
        {
            int status = _dictionaryService.DeleteCounty(countyToDeleteId);
            if (status != 500)
                return Json(status);
            else
                return View();
            // return RedirectToAction("Counties");
        }


        public IActionResult DictionarySeasonType()
        {
            return View();
        }


        public void DeleteCountry([DataSourceRequest] DataSourceRequest request, DictionaryCountry model)
        {
            _dictionaryService.DeleteCountry(model.DictionaryCountryId);
        }

        public IActionResult Country(int CountryId)
        {
            DictionaryCountry dictionaryCountry = _dictionaryService.GetSelectedCountry(CountryId);
            return View(dictionaryCountry);
        }

        public ActionResult DictionarySeasonTypeData([DataSourceRequest]DataSourceRequest request, string SeasonName, string SeasonCode)//************
        {
            if (string.IsNullOrEmpty(SeasonName) == true && string.IsNullOrEmpty(SeasonCode) == true)
            {
                var jsonVar = _dictionaryService.GetDictionarySeasonTypeModels(request.Page, request.PageSize);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetDictionarySeasonTypeModels() // Total number of records
                };
                return Json(result);
            }
            else if (string.IsNullOrEmpty(SeasonName) == false && string.IsNullOrEmpty(SeasonCode) == true)
            {
                var jsonVar = _dictionaryService.FilterDictionarySeasonTypeByName(request.Page, request.PageSize, SeasonName);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetDictionarySeasonTypeCount() // Total number of records
                };
                return Json(result);
            }
            else if (string.IsNullOrEmpty(SeasonName) == true && string.IsNullOrEmpty(SeasonCode) == false)
            {
                var jsonVar = _dictionaryService.FilterDictionarySeasonTypeByCode(request.Page, request.PageSize, SeasonCode);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetDictionarySeasonTypeCount() // Total number of records
                };
                return Json(result);
            }
            else
            {
                var jsonVar = _dictionaryService.FilterDictionarySeasonTypeByCodeAndName(request.Page, request.PageSize, SeasonName, SeasonCode);
                DataSourceResult result = new DataSourceResult()
                {
                    Data = jsonVar, // Process data (paging and sorting applied)
                    Total = _dictionaryService.GetDictionarySeasonTypeCount() // Total number of records
                };
                return Json(result);
            }
        }
        ///functie stergere Sezoane
        public void DeleteSeason([DataSourceRequest] DataSourceRequest request, DictionarySeasonType model)
        {

            _dictionaryService.DeleteSeason(model.DictionarySeasonId);
        }

        public ActionResult ServerFiltering_GetCounties(string text)
        {
            return Json(_dictionaryService.ServerFiltering_GetCounties(text));
        }

        [HttpPost]
        public ActionResult SaveCity(string cityName, int countyId, string cityCode)
        {
            DictionaryCity city = new DictionaryCity()
            {

                DictionaryCityName = cityName,
                DictionaryCountyId = countyId,
                DictionaryCityCode = cityCode
            };
            int status = _dictionaryService.InsertCity(city);
            if (status != 500)
                return Json(status);
            else return View();
        }

        public ActionResult DeleteCity([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {
                _dictionaryService.DeleteCity(id);
            }
            return Json(ModelState.ToDataSourceResult());

        }

        public IActionResult City(int id)
        {
            if (id != 0)
            {
                DictionaryCityModel c = _dictionaryService.GetSelectedCity(id);
                return View(c);
            }
            else
            {
                DictionaryCityModel c = new DictionaryCityModel();
                return View(c);
            }
        }

        public ActionResult EditCity(string cityName, int cityId, string cityCode, int countyId)
        {
            DictionaryCity city = new DictionaryCity()
            {
                DictionaryCityName = cityName,
                DictionaryCityId = cityId,
                DictionaryCountyId = countyId,
                DictionaryCityCode = cityCode
            };
            int status = _dictionaryService.UpdateCity(city);
            if (status != 500)
                return Json(status);
            else
                return View();

        }
        public void DeleteLandmark([DataSourceRequest] DataSourceRequest request, int landmarkToDelete)
        {
            _dictionaryService.DeleteLandmark(landmarkToDelete);
        }

        public ActionResult County(int countyToEditId)
        {
            if (countyToEditId == 0)
            {
                return View(new DictionaryCountyModel());

            }
            else
            {
                DictionaryCountyModel countyToEdit = new DictionaryCountyModel();
                countyToEdit = _dictionaryService.getInfoCounty(countyToEditId);
                return View(countyToEdit);
            }
            //return Json(status);
        }

        public IActionResult UpdateSeason(DictionarySeasonType dictionarySeason)
        {

            string status = "";
            _dictionaryService.UpdateSeason(dictionarySeason);
            return Json(status);

        }


        public ActionResult InsertSeason(DictionarySeasonType dictionarySeason)
        {
            string status = "";
            _dictionaryService.InsertSeason(dictionarySeason);
            return Json(status);

        }
        public IActionResult EditSeason(int SeasonId)
        {

            DictionarySeasonType dictionarySeason = _dictionaryService.GetSelectedSeason(SeasonId);
            return View(dictionarySeason);
        }

        public ActionResult AddCounty([DataSourceRequest] DataSourceRequest request, string countyName, string countyCode,
            int countryId)
        {
            int status = _dictionaryService.AddCounty(countyName, countyCode, countryId);
            if (status != 500)
                return Json(status);
            else
                return View();
        }

        public ActionResult CurrencyData([DataSourceRequest]DataSourceRequest request, string currencyName)
        {
            List<DictionaryCurrencyType> data = _dictionaryService.GetDictionaryCurrencyTypeModels(request.Page, request.PageSize, currencyName);
            var result = new DataSourceResult()
            {
                Data = data,
                Total = _dictionaryService.GetDictionaryCurrencyTypeModels1(request.Page, request.PageSize, currencyName)
            };

            return Json(result);
        }

        public ActionResult DeleteCurrency([DataSourceRequest] DataSourceRequest request, int id)
        {
            int status = 0;
            if (id != 0)
            {
                status = _dictionaryService.DeleteCurrency(id);
            }
            if (status != 500)
            {
                return Json(ModelState.ToDataSourceResult());
            }
            else return View();

        }
        public ActionResult EditCounty([DataSourceRequest] DataSourceRequest request, int countyId, string countyName,
            string countyCode, int countryId)
        {
            int status = _dictionaryService.EditCounty(countyId, countyName, countyCode, countryId);
            if (status != 500)
                return Json(status);
            else
                return View();
        }

        public IActionResult UpdateCountry(DictionaryCountry dictionaryCountry)
        {
            string status = "";
            _dictionaryService.UpdateCountry(dictionaryCountry);
            return Json(status);
        }

        public ActionResult InsertCountry(DictionaryCountry dictionaryCountry)
        {
            string status = "";
            _dictionaryService.InsertCountry(dictionaryCountry);
            return Json(status);
        }
        public IActionResult CurrencyAdd(int id)
        {
            if (id != 0)
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
        public ActionResult EditCurrency(int typeId, string currencyName, string currencyCode)
        {
            DictionaryCurrencyType dictionary = new DictionaryCurrencyType();

            dictionary.DictionaryCurrencyTypeId = typeId;
            dictionary.DictionaryCurrencyName = currencyName;
            dictionary.DictionaryCurrencyCode = currencyCode;

            // DatabaseContext database = new DatabaseContext();

            _dbContext.DictionaryCurrencyType.Update(dictionary);

            return Json(_dbContext.SaveChanges());
        }
        public ActionResult CurrencySave(string currencyName, string currencyCode)
        {
            // DatabaseContext database = new DatabaseContext();
            _dbContext.DictionaryCurrencyType.Add(new DictionaryCurrencyType
            {
                DictionaryCurrencyName = currencyName,
                DictionaryCurrencyCode = currencyCode
            });
            return Json(_dbContext.SaveChanges());
        }



        public async void BNR_Integration([DataSourceRequest] DataSourceRequest request)
        {
            ServiceBNR.CursSoapClient curs = new ServiceBNR.CursSoapClient(ServiceBNR.CursSoapClient.EndpointConfiguration.CursSoap);
            ServiceBNR.getallResponseGetallResult result = curs.getallAsync(DateTime.UtcNow).Result;

            //_dictionaryService.AddBNRCurrency(new Currrency() { Value = 3, CurrencyDate = DateTime.Now });

            foreach (var xNode in result.Any1.Elements().Where(x => x.Value != "").FirstOrDefault().Nodes())
            {
                var elem = xNode as XElement;
                var valuta = elem.Element("IDMoneda");
                var valoare = elem.Element("Value");
                //var data = elem.Element("CurrencyDate");
                // var data = elem.Element("");

                if (valuta != null && valuta.Value != null)

                    _dictionaryService.AddBNRType(new DictionaryCurrencyType()
                    {
                        DictionaryCurrencyName = valuta.Value,
                        DictionaryCurrencyCode = valuta.Value

                    });

                if (valoare != null && valoare.Value != null)

                    _dictionaryService.AddBNRCurrency(new Currency()
                    {

                        Value = double.Parse(valoare.Value),
                        CurrencyDate = DateTime.UtcNow

                    },new DictionaryCurrencyType()
                    {
                        DictionaryCurrencyName = valuta.Value,
                        DictionaryCurrencyCode = valuta.Value

                    });
               

            }




        }

    }




}
