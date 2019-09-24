using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace iWasHere.Domain.Service
{
    public class AttractionService
    {
        private readonly DatabaseContext _dbContext;
        private static bool UpdateDatabase = false;

        public AttractionService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public AttractionModel GetAttractionLocation(int attractionId)
        {
            AttractionModel attr = _dbContext.Attractions.Select(a => new AttractionModel()
            {
                AttractionId = a.AttractionId,
                CurrencyId = a.CurrencyId,
                CityId = a.CityId,
                Price = a.Price,
                LandmarkTypeId = a.LandmarkTypeId,
                AttractionTypeId = a.AttractionTypeId,
                SeasonId = a.SeasonId,
                AttractionName = a.AttractionName,
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                Observations = a.Observations,
                CityName = a.City.DictionaryCityName,
                AttractionTypeName = a.AttractionType.DictionaryAttractionName,               
                //Currency = a.Currency,
                LandmarkTypeName = a.LandmarkType.DictionaryItemName, 
                SeasonName = a.Season.DictionarySeasonName,
                //Comment = a.Comment,
                //Photo = a.Photo
            })
            .Where(a => a.AttractionId == attractionId)
            .FirstOrDefault();

            return attr;
        }
    }
}
