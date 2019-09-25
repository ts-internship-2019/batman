﻿using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using Microsoft.EntityFrameworkCore;
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


        public AttractionService(DatabaseContext databaseContext)
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
        public List<DictionaryCountyModel> ServerFiltering_GetCounties(int? countryId, string text)
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
            if (countyId != 0)
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

        public IQueryable<AttractionListModel> GetAttractionListModels()
        {
            var x =

            _dbContext.Attractions.Include(a => a.Currency)
            .Include(a => a.LandmarkType)
            .Include(a => a.AttractionType)
            .Include(a => a.Season)
            .Include(a => a.Photo)
            .Include(a => a.Comment)
            .Include(a => a.City)
            .Select(a => new AttractionListModel()
            {
                AttractionId = a.AttractionId,
                AttractionTypeName = a.AttractionType.DictionaryAttractionName,
                CurrencyName = a.Currency.DictionaryCurrencyCode,
                LandmarkTypeName = a.LandmarkType.DictionaryItemName,
                MainPhotoName = a.Photo.Any() ? a.Photo.FirstOrDefault().PhotoName : null,
                Name = a.AttractionName,
                Observations = a.Observations,
                Price = a.Price,
                Rating = (a.Comment.Any() ? a.Comment.Average(b => b.Rating) : (double?)null),
                CityName = a.City.DictionaryCityName,
                SeasonName = a.Season.DictionarySeasonName
            });

            return x;
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
                Comment = a.Comment,
                Photo = a.Photo
            })
            .Where(a => a.AttractionId == attractionId)
            .FirstOrDefault();

            return attr;
        }
    }
}
