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
        public List<DictionaryCityModel> ServerFiltering_GetCities(string text)
        {
            var x = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.DictionaryCityId,
                Name = a.DictionaryCityName
            });
            if (!string.IsNullOrEmpty(text))
            {
                x = x.Where(p => p.Name.StartsWith(text));
            }
            List<DictionaryCityModel> dictionaryCountyModels = x.Take(50).ToList();

            return dictionaryCountyModels;
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
                CurrencyName = a.Currency.CurrencyType.DictionaryCurrencyName,
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
                CountryName = a.City.DictionaryCounty.DictionaryCountry.DictionaryCountryName,                
                AttractionTypeName = a.AttractionType.DictionaryAttractionName,               
                //Currency = a.Currency,
                LandmarkTypeName = a.LandmarkType.DictionaryItemName, 
                SeasonName = a.Season.DictionarySeasonName,
                Comment = a.Comment,
                
                Photo = a.Photo,
                CountryId = a.City.DictionaryCounty.DictionaryCountry.DictionaryCountryId
            })
            .Where(a => a.AttractionId == attractionId)
            .FirstOrDefault();

            return attr;
        }

        public AttractionModel GetAttractionToExport(int attractionId)
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
                CountyName = a.City.DictionaryCounty.DictionaryCountyName,
                CountryName = a.City.DictionaryCounty.DictionaryCountry.DictionaryCountryName,
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
        public void SaveAttraction(AttractionModel attractionModel,out string errorMessage,out int id)
        {
            Attractions attractions = new Attractions();
            attractions.CurrencyId = 1;
            errorMessage = "";
            if (attractionModel.AttractionId != 0)
                attractions.AttractionId = attractionModel.AttractionId;
            if (!(string.IsNullOrWhiteSpace(attractionModel.AttractionName)))
                attractions.AttractionName = attractionModel.AttractionName;
            if (!(string.IsNullOrWhiteSpace(attractionModel.Observations)))
                attractions.Observations = attractionModel.Observations;
            if (!string.IsNullOrWhiteSpace(attractionModel.Latitude))
                attractions.Latitude = attractionModel.Latitude;
            if (!string.IsNullOrWhiteSpace(attractionModel.Longitude))
                attractions.Longitude = attractionModel.Longitude;
            if (attractionModel.Price != 0)
                attractions.Price = attractionModel.Price;
           
            if (attractionModel.AttractionTypeId != 0)
                attractions.AttractionTypeId = attractionModel.AttractionTypeId;
            if (attractionModel.CityId != 0)
                attractions.CityId = attractionModel.CityId;
            if (attractionModel.CurrencyId != 0)
                attractions.CurrencyId = attractionModel.CurrencyId;
            
            if (attractionModel.LandmarkTypeId != 0)
                attractions.LandmarkTypeId = attractionModel.LandmarkTypeId;
            if (attractionModel.SeasonId != 0)
                attractions.SeasonId = attractionModel.SeasonId;
          

            if (attractionModel.AttractionId == 0)
            {
                _dbContext.Attractions.Add(attractions);
            }
            else
            {
                _dbContext.Attractions.Update(attractions);
            }
            
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                errorMessage = "Ceva nu a mers.Mai incearca odata!!!";
            }
            id = attractions.AttractionId;
        }
        public List<PhotoModel> GetPhotosByAttractionId(int attractionId)
        {

            var x = _dbContext.Photo.Select(a => new PhotoModel()
            {
                PhotoId = a.PhotoId,
                AttractionId = (int)a.AttractionId,
                PhotoName = a.PhotoName
                //Photo = a.Photo
            })
            .Where(a => a.AttractionId == attractionId);

            List<PhotoModel> photos = x.ToList();
            return photos;                                                      
        }
        public int AddPhoto(int attractionId, string photoName)
        {
            int status = 0;
            Photo image = new Photo()
            {
                AttractionId = attractionId,
                PhotoName = photoName               
            };
            try
            {
                _dbContext.Photo.Add(image);
                status = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                status = 500;
            }
            return status;
        }

        public List<AttractionListModel> GetAttractionsFromCountry(int? countryId)
        {
            var x = _dbContext.Attractions.Select(a => new AttractionListModel()
            {
                AttractionId = a.AttractionId,
                AttractionTypeName = a.AttractionType.DictionaryAttractionName,
                CurrencyName = a.Currency.CurrencyType.DictionaryCurrencyName,
                LandmarkTypeName = a.LandmarkType.DictionaryItemName,
                MainPhotoName = a.Photo.Any() ? a.Photo.FirstOrDefault().PhotoName : null,
                Name = a.AttractionName,
                Observations = a.Observations,
                Price = a.Price,
                Rating = (a.Comment.Any() ? a.Comment.Average(b => b.Rating) : (double?)null),
                CityName = a.City.DictionaryCityName,
                SeasonName = a.Season.DictionarySeasonName,
                CountryId = a.City.DictionaryCounty.DictionaryCountry.DictionaryCountryId
            });

            if (countryId.HasValue)
            {
                x = x.Where(p => p.City.DictionaryCounty.DictionaryCountry.DictionaryCountryId == countryId);
            }

            List<AttractionListModel> attractionsCountryModels = x.ToList();

            return attractionsCountryModels;
        }
        public void SaveImagesDB(string path, int id,out string errorMessage)
        {
            int status = 0;
            errorMessage = "";
            Photo photo = new Photo()
            {
                PhotoName = path,
                AttractionId = id
            };
            
            try
            {
                _dbContext.Photo.Add(photo);
               status =  _dbContext.SaveChanges();
                
            }
            catch (Exception)
            {
                errorMessage = "ceva merge prost la imagini";
                status = 500;
            }

        }
        public List<DictionaryAttractionType> GetAttractionTypesforCombo()
        {
            var attr = _dbContext.DictionaryAttractionType.Select(a => new DictionaryAttractionType()
            {
                DictionaryAttractionName = a.DictionaryAttractionName,
                DictionaryAttractionTypeId = a.DictionaryAttractionTypeId

            });
            return attr.Take(50).ToList();
        }
        //public int GetAttractionsByNameObsLatLong(string name, string obs, string lat , string longi)
        //{
        //    //Attractions x = _dbContext.Attractions.Where(a => a.AttractionName == name);
            
        //    return x.AttractionId;
        //}

        public int AddComment(CommentModel comentariu)
        {
            int status = 0;
             if(comentariu.numeuser == null)
                {
                status = 500;
                }

            if (status != 500)
            {
                Comment comment = new Comment()
                {
                    UserId = comentariu.UserID,
                    Name = comentariu.numeuser,
                    AttractionId = comentariu.attractionid,
                    Rating = comentariu.rating,
                    CommentTitle = comentariu.titlu,
                    CommentText = comentariu.descriere,
                };


                try
                {
                    _dbContext.Comment.Add(comment);
                    status = _dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    status = 500;
                }
            }
            return status;
        }
    }
}
