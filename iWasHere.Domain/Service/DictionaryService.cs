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

        public List<DictionaryCityModel> GetDictionaryCity()
        {
            int skip = (page-1) * pageSize;
            
            List<DictionaryCityTypeModel> dictionaryCity = _dbContext.DictionaryCity.Select(a => new DictionaryCityTypeModel()
            //int skip = (page - 1) * pageSize;
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
        
    }
}
