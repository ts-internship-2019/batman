using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

     

        public int GetCurrencyCount()
        {
            List<DictionaryCurrencyType> dictionaryCurrencyTypeModels = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyType()

            {
                DictionaryCurrencyName = a.DictionaryCurrencyName,
                DictionaryCurrencyCode = a.DictionaryCurrencyCode,
                DictionaryCurrencyTypeId = a.DictionaryCurrencyTypeId
               

            }).ToList();
            return dictionaryCurrencyTypeModels.Count;
        }



       

        public int GetDictionaryCurrencyTypeModels1(int Page, int PageSize, string currencyName)
        {

            int skip = (Page - 1) * PageSize;
            var x = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyType()

            {
                DictionaryCurrencyName = a.DictionaryCurrencyName,
                DictionaryCurrencyCode = a.DictionaryCurrencyCode,

                DictionaryCurrencyTypeId = a.DictionaryCurrencyTypeId,
               
            });
            if (!string.IsNullOrEmpty(currencyName))
            {
                x = x.Where(p => p.DictionaryCurrencyName.Contains(currencyName));
            }
            List<DictionaryCurrencyType> dictionaryCurrencyTypeModels = x.ToList();
            return dictionaryCurrencyTypeModels.Count;

        }




        public List<DictionaryCurrencyType>  GetDictionaryCurrencyTypeModels(int Page, int PageSize, string currencyName)
        {
            
            int skip = (Page - 1) * PageSize;
            var x = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyType()

            {
                DictionaryCurrencyName = a.DictionaryCurrencyName,
                DictionaryCurrencyCode = a.DictionaryCurrencyCode,

                DictionaryCurrencyTypeId = a.DictionaryCurrencyTypeId,
            

            });
            if(!string.IsNullOrEmpty(currencyName))
            {
                x = x.Where(p => p.DictionaryCurrencyName.Contains(currencyName));
            }
            List<DictionaryCurrencyType> dictionaryCurrencyTypeModels = x.Skip(skip).Take(PageSize).ToList();
                    
            return dictionaryCurrencyTypeModels;

        }








        public List<DictionaryCurrencyTypeModel> GetAllCurrencyList()
        {
            List<DictionaryCurrencyTypeModel> dictionaryCountryModels = _dbContext.Currrency.Select(a => new DictionaryCurrencyTypeModel()
            {
                DicurrencyId = a.CurrencyType.DictionaryCurrencyTypeId,

                Name = a.CurrencyType.DictionaryCurrencyName,
                Code = a.CurrencyType.DictionaryCurrencyCode,

                Id = a.CurrencyType.DictionaryCurrencyTypeId,
             
            }).ToList();

            return dictionaryCountryModels;
        }





        
        public void DeleteCurrency(int id)
        {



            DictionaryCurrencyType getDictionaryId = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyType()
            {
                DictionaryCurrencyTypeId = a.DictionaryCurrencyTypeId,
                

            }).Where(a => a.DictionaryCurrencyTypeId.Equals(id)).FirstOrDefault();

           

            DictionaryCurrencyType dictionary = new DictionaryCurrencyType() { DictionaryCurrencyTypeId = getDictionaryId.DictionaryCurrencyTypeId };

            _dbContext.DictionaryCurrencyType.Remove(dictionary);

            _dbContext.SaveChanges();

        }





        public DictionaryCurrencyType CurrencyEdit(int id)
        {
          
            
            DictionaryCurrencyType currency1 = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyType()
            {
               
                DictionaryCurrencyTypeId = a.DictionaryCurrencyTypeId,
                DictionaryCurrencyName = a.DictionaryCurrencyName,
                DictionaryCurrencyCode=a.DictionaryCurrencyCode
               


            }).Where(a => a.DictionaryCurrencyTypeId == id).FirstOrDefault();

            
           
            
            return currency1;
        }

        




    }
}
