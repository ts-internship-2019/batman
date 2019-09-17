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

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).ToList();

            return dictionaryLandmarkTypeModels;
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
