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

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).ToList();

            return dictionaryLandmarkTypeModels;
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