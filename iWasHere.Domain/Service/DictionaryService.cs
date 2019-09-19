using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace iWasHere.Domain.Service
{
    public class DictionaryService 
    {

        private readonly DatabaseContext _dbContext;
        private static bool UpdateDatabase = false;


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

        public object GetDictionarySeasonTypeAndCode()
        {
            throw new NotImplementedException();
        }

        public List<DictionaryCityModel> GetDictionaryCity(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;



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

        public int GetDictionarySeasonTypeCount()
        {
            return _dbContext.DictionarySeasonType.Count();

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

        public List<DictionarySeasonType> GetDictionarySeasonTypeModelsbyId(int Page, int PageSize,int DictionarySeasonId)
        {
            int skip = (Page - 1) * PageSize;
            List<DictionarySeasonType> dictionarySeasonTypeModels = _dbContext.DictionarySeasonType.Select(a => new DictionarySeasonType()
            {
                DictionarySeasonId = a.DictionarySeasonId,
                DictionarySeasonCode = a.DictionarySeasonCode,
                DictionarySeasonName = a.DictionarySeasonName

            }).Where(a=> a.DictionarySeasonId == DictionarySeasonId).Skip(skip).Take(PageSize).ToList();

            return dictionarySeasonTypeModels;

        }
        public int GetDictionarySeasonTypeModels()
        {
            return _dbContext.DictionarySeasonType.Count();
        }

        public List<DictionarySeasonType> FilterDictionarySeasonTypeByName(int page, int pageSize, string SeasonName)
        {
            List<DictionarySeasonType> filterDictionarySeasonTypeModels = _dbContext.DictionarySeasonType
         .Where(a => a.DictionarySeasonName == SeasonName || a.DictionarySeasonName.StartsWith(SeasonName))
              

         .Select(a => new DictionarySeasonType()
         {
             DictionarySeasonName = a.DictionarySeasonName,
             DictionarySeasonCode = a.DictionarySeasonCode
         }).ToList();
            return filterDictionarySeasonTypeModels;
        }

        public List<DictionarySeasonType> FilterDictionarySeasonTypeByCode(int page, int pageSize, string SeasonCode)
        {
            List<DictionarySeasonType> filterDictionarySeasonTypeModels = _dbContext.DictionarySeasonType
         .Where(a => a.DictionarySeasonCode == SeasonCode || a.DictionarySeasonCode.StartsWith(SeasonCode))


         .Select(a => new DictionarySeasonType()
         {
             DictionarySeasonName = a.DictionarySeasonName,
             DictionarySeasonCode = a.DictionarySeasonCode
         }).ToList();
            return filterDictionarySeasonTypeModels;
        }

        public List<DictionarySeasonType> FilterDictionarySeasonTypeByCodeAndName(int page, int pageSize, string SeasonName, string SeasonCode)
        {
            List<DictionarySeasonType> filterDictionarySeasonTypeModels = _dbContext.DictionarySeasonType
         .Where(a => a.DictionarySeasonCode == SeasonCode || a.DictionarySeasonCode.StartsWith(SeasonCode))
         .Where(a => a.DictionarySeasonName == SeasonName || a.DictionarySeasonName.StartsWith(SeasonName))

         .Select(a => new DictionarySeasonType()
         {
             DictionarySeasonName = a.DictionarySeasonName,
             DictionarySeasonCode = a.DictionarySeasonCode
         }).ToList();
            return filterDictionarySeasonTypeModels;
        }




        //Stergere sezoane
        public void DeleteSeason (int SeasonId)
        {
            using (_dbContext)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope ())
                    {
                        var Season = from ses in _dbContext.DictionarySeasonType
                                     where ses.DictionarySeasonId == SeasonId
                                     select ses;
                        if (Season != null)
                        {
                            foreach (var item in Season)
                            _dbContext.DictionarySeasonType.Remove(item);

                        }
                        _dbContext.SaveChanges();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }



        }
    }




}