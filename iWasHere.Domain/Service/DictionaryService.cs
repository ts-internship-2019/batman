using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly DatabaseContext _dbContext;
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
        public DictionaryLandmarkType GetSelectedLandmark(int id)
        {
            if (id == 0 )
            {
                return null;
            }
            List<DictionaryLandmarkType> dictionaryLandmarkTypes = _dbContext.DictionaryLandmarkType.Where(a => a.DictionaryItemId == id).Select(a => new DictionaryLandmarkType()

            {
                DictionaryItemId = a.DictionaryItemId,
                DictionaryItemCode = a.DictionaryItemCode,
                DictionaryItemName = a.DictionaryItemName,
                Description = a.Description


            }).ToList();
            return dictionaryLandmarkTypes[0];
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
            return _dbContext.DictionaryLandmarkType.Count();
        }
        public int GetCountryCount()
        {
            return _dbContext.DictionaryCountry.Count();
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

        public List<DictionaryCountry> FilterCountriesByName(int page, int pageSize, string CountryName)//filtrare dupa nume
        {
            List<DictionaryCountry> filterDictionaryCountryModels = _dbContext.DictionaryCountry
                .Where(a => a.DictionaryCountryName == CountryName || a.DictionaryCountryName.StartsWith(CountryName))
                .Select(a => new DictionaryCountry()
                {
                    DictionaryCountryId = a.DictionaryCountryId,
                    DictionaryCountryCode = a.DictionaryCountryCode,
                    DictionaryCountryName = a.DictionaryCountryName,
                }).ToList();

            return filterDictionaryCountryModels;
        }

        public List<DictionaryCountry> FilterCountriesByCode(int page, int pageSize, string CountryCode)//filtrare dupa cod
        {
            List<DictionaryCountry> filterDictionaryCountryModels = _dbContext.DictionaryCountry
                .Where(a => a.DictionaryCountryCode == CountryCode || a.DictionaryCountryCode.StartsWith(CountryCode))
                .Select(a => new DictionaryCountry()
                {
                    DictionaryCountryId = a.DictionaryCountryId,
                    DictionaryCountryCode = a.DictionaryCountryCode,
                    DictionaryCountryName = a.DictionaryCountryName,
                }).ToList();

            return filterDictionaryCountryModels;
        }

        public List<DictionaryCountry> FilterCountriesByCodeAndName(int page, int pageSize, string CountryName, string CountryCode)//filtrare dupa nume si cod
        {
            List<DictionaryCountry> filterDictionaryCountryModels = _dbContext.DictionaryCountry
                .Where(a => a.DictionaryCountryName == CountryName || a.DictionaryCountryName.StartsWith(CountryName))
                .Where(a => a.DictionaryCountryCode == CountryCode || a.DictionaryCountryCode.StartsWith(CountryCode))
                .Select(a => new DictionaryCountry()
                {
                    DictionaryCountryId = a.DictionaryCountryId,
                    DictionaryCountryCode = a.DictionaryCountryCode,
                    DictionaryCountryName = a.DictionaryCountryName,
                }).ToList();

            return filterDictionaryCountryModels;
        }

        public List<DictionarySeasonType> GetDictionarySeasonTypeModels(int Page, int PageSize)
        {
            int skip = (Page - 1) * PageSize;
            List<DictionarySeasonType> dictionarySeasonTypeModels = _dbContext.DictionarySeasonType.Select(a => new DictionarySeasonType()
            {
                DictionarySeasonId = a.DictionarySeasonId,
                DictionarySeasonCode = a.DictionarySeasonCode,
                DictionarySeasonName = a.DictionarySeasonName

            }).Skip(skip).Take(PageSize).ToList();

            return dictionarySeasonTypeModels;



        }
    }
}