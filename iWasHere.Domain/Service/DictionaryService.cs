using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

namespace iWasHere.Domain.Service
{
    public class DictionaryService 
    {
        private readonly DatabaseContext _dbContext;

        //private static bool UpdateDatabase = false;
        //private SampleEntities entities;

        //public DictionaryService(SampleEntities entities)
        //{
        //    this.entities = entities;
        //}




        public DictionaryService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels(int page, int pageSize)
        {
            int skip = (page - 1) * (pageSize);
            //List<DictionaryLandmarkTypeModel> total = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            //{
            //    Id = a.DictionaryItemId,
            //    Name = a.DictionaryItemName
            //}).ToList();
            
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).Skip(skip).Take(pageSize).ToList();


            return dictionaryLandmarkTypeModels;
        }

        public List<DictionaryCityModel> GetDictionaryCity(int page, int pageSize)
        {
            int skip = (page-1) * pageSize;
            
            
            
            List<DictionaryCityModel> dictionaryCity = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.DictionaryCityId,
                Name = a.DictionaryCityName,
                CountyId = a.DictionaryCounty.DictionaryCountyId,
                CountyName = a.DictionaryCounty.DictionaryCountyName

            }).ToList();

            return dictionaryCity;
        }
        public int GetLandmarkCount()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).ToList();
            return dictionaryLandmarkTypeModels.Count;
        }
        public int GetCountryCount()
        {
            List<DictionaryCountry> dictionaryCountry = _dbContext.DictionaryCountry.Select(a => new DictionaryCountry()
            {
                DictionaryCountryId = a.DictionaryCountryId,
                DictionaryCountryCode = a.DictionaryCountryCode,
                DictionaryCountryName = a.DictionaryCountryName

            }).ToList();
            return dictionaryCountry.Count;
        }

        public List<DictionaryCountry> GetDictionaryCountry(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            List<DictionaryCountry> dictionaryCountry = _dbContext.DictionaryCountry.Select(a => new DictionaryCountry()
            {
                DictionaryCountryId = a.DictionaryCountryId,
                DictionaryCountryCode = a.DictionaryCountryCode,
                DictionaryCountryName = a.DictionaryCountryName

            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCountry;
        }

        public List<DictionaryCountyModel> GetDictionaryCountyModels(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            List<DictionaryCountyModel> dictionaryCountyModels = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName,
                CountyCode = a.DictionaryCountyCode,
                CountryId = a.DictionaryCountry.DictionaryCountryId,
                CountryName = a.DictionaryCountry.DictionaryCountryName
            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCountyModels;
        }        

        public int GetDictionaryCountyCount()
        {
            return _dbContext.DictionaryCounty.Count();          
            
        }

        public List<DictionaryCountyModel> FilterDictionaryCountyModels(string searchCountyName, string searchCountyCode, string searchCountryName, int page, int pageSize)
        {
            List<DictionaryCountyModel> filterDictionaryCountyModels = _dbContext.DictionaryCounty
                .Where(a => a.DictionaryCountyName == searchCountyName || a.DictionaryCountyCode == searchCountyCode ||
                    a.DictionaryCountry.DictionaryCountryName == searchCountryName)
                .Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName,
                CountyCode = a.DictionaryCountyCode,
                CountryId = a.DictionaryCountry.DictionaryCountryId,
                CountryName = a.DictionaryCountry.DictionaryCountryName
            }).ToList();

            return filterDictionaryCountyModels;
        }

        public List<DictionaryCountryModel> GetCountryList()
        {
            List<DictionaryCountryModel> dictionaryCountryModels = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                CountryId = a.DictionaryCountryId,
                CountryName = a.DictionaryCountryName,
                CountryCode = a.DictionaryCountryCode
            }).ToList();

            return dictionaryCountryModels;
        }

        public List<DictionaryCountryModel> ServerFiltering_GetCountries(string text)
        {
            var x = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                CountryId = a.DictionaryCountryId,
                CountryName = a.DictionaryCountryName,
                CountryCode = a.DictionaryCountryCode
            });
            if (!string.IsNullOrEmpty(text))
            {
                x = x.Where(p => p.CountryName.StartsWith(text));
            }
            List<DictionaryCountryModel> dictionaryCountryModels = x.ToList();                                            
                       
            return dictionaryCountryModels;
        }

        public List<DictionaryCurrencyTypeModel> GetDictionaryCurrencyTypeModels(int Page, int PageSize)
        {
            List<DictionaryCurrencyTypeModel> dictionaryCurrencyTypeModels = _dbContext.Currrency.Select(a => new DictionaryCurrencyTypeModel()
            {
                Name = a.CurrencyType.DictionaryCurrencyName,
                Code = a.CurrencyType.DictionaryCurrencyCode,
                Id = a.CurrencyType.DictionaryCurrencyTypeId,
                Value = a.Value,
                Data = a.CurrencyDate

            }).ToList();
            return dictionaryCurrencyTypeModels;

        }


        //public List<DictionaryCurrency> GetDictionaryCurrencies(int Page, int PageSize)
        //{
        //    List<DictionaryCurrency> dictionaryCurrencies = _dbContext.Currrency.Select(a => new DictionaryCurrency()
        //    {
        //        CurrencyId = a.CurrencyId,
        //        TypeId = a.CurrencyType.DictionaryCurrencyTypeId,
        //        Value = a.Value,
        //        Date = a.CurrencyDate
        //    }).ToList();

        //    return dictionaryCurrencies;

        //}


        //public List<DictionaryCurrencyTypeModel> GetDictionaryCurrencyTypeModels()
        //{
        //    var result = HttpContext.Current.Session["Currency"] as IList<DictionaryCurrencyTypeModel>;


        //}





    }
}
