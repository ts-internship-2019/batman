using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace iWasHere.Domain.Service
{
    public class AtractionService
    {

        private readonly DatabaseContext _dbContext;
        private static bool UpdateDatabase = false;


        public AtractionService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
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
            List<DictionaryCountryModel> dictionaryCountryModels = x.Take(50).ToList();

            return dictionaryCountryModels;
        }
        public List<DictionaryCountyModel> ServerFiltering_GetCounties(int ? countryId, string text)
        {
            var x = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName,
                CountyCode = a.DictionaryCountyCode,
                CountryId = (int)a.DictionaryCountryId
            });
            if (!string.IsNullOrEmpty(text))
            {
                x = x.Where(p => p.CountyName.StartsWith(text));
            }
            if (countryId != 0)
            {
                x = x.Where(p => p.CountryId.Equals(countryId));
            }
            List<DictionaryCountyModel> dictionaryCountyModels = x.Take(50).ToList();

            return dictionaryCountyModels;
        }
        public List<DictionaryCityModel> ServerFiltering_GetCities(int? countyId, string text)
        {
            var x = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.DictionaryCityId,
                Name = a.DictionaryCityName,
                CountyId = (int)a.DictionaryCountyId
            });
            if (!string.IsNullOrEmpty(text))
            {
                x = x.Where(p => p.CountyName.StartsWith(text));
            }
            if (countyId != 0 )
            {
                x = x.Where(p => p.CountyId.Equals(countyId));
            }
            List<DictionaryCityModel> dictionaryCityModels = x.Take(50).ToList();

            return dictionaryCityModels;
        }
        public List<DictionarySeasonType> ServerFiltering_GetSeasons(string text)
        {
            var x = _dbContext.DictionarySeasonType.Select(a => new DictionarySeasonType()
            {
                DictionarySeasonName = a.DictionarySeasonName,
                DictionarySeasonCode = a.DictionarySeasonCode,
                DictionarySeasonId = a.DictionarySeasonId
            });
            if (!string.IsNullOrEmpty(text))
            {
                x = x.Where(p => p.DictionarySeasonName.StartsWith(text));
            }
            List<DictionarySeasonType> dictionarySeason = x.Take(50).ToList();

            return dictionarySeason;
        }
        
        public List<DictionaryLandmarkType> ServerFiltering_GetLandmarks(string text)
        {
            var x = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkType()
            {
                DictionaryItemId = a.DictionaryItemId,
                DictionaryItemCode = a.DictionaryItemCode,
                DictionaryItemName = a.DictionaryItemName,
                Description = a.Description
            });
            if (!string.IsNullOrEmpty(text))
            {
                x = x.Where(p => p.DictionaryItemName.StartsWith(text));
            }
            List<DictionaryLandmarkType> dictionaryLandmarkModels = x.ToList();

            return dictionaryLandmarkModels;
        }


    }
}
