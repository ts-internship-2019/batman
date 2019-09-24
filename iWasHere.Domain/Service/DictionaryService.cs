using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using Microsoft.EntityFrameworkCore;
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

            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryLandmarkTypeModels;
        }
        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkModels(int page, int pageSize,
           int? landmarkId, string landmarkName, string landmarkCode, out int landmarkCount)
        {
            int skip = (page - 1) * pageSize;
            var x = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Code = a.DictionaryItemCode,
                Name = a.DictionaryItemName

            });

            if (landmarkId.HasValue)
            {
                x = x.Where(p => p.Id == landmarkId);
            }

            if (!string.IsNullOrEmpty(landmarkName))
            {
                x = x.Where(p => p.Name.StartsWith(landmarkName));
            }

            if (!string.IsNullOrEmpty(landmarkCode))
            {
                x = x.Where(p => p.Code.StartsWith(landmarkCode));
            }

            landmarkCount = x.Count();

            List<DictionaryLandmarkTypeModel> dictionaryLandmarkModels = x.Skip(skip).Take(pageSize).ToList();

            return dictionaryLandmarkModels;
        }

        public DictionaryLandmarkType GetSelectedLandmark(int id)
        {
            if (id == 0)
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
        public void InsertLandmark(DictionaryLandmarkType landmarkType)
        {

            _dbContext.DictionaryLandmarkType.Add(landmarkType);
            _dbContext.SaveChanges();

        }
        public void UpdateLandmark(DictionaryLandmarkType landmarkType)
        {

            _dbContext.DictionaryLandmarkType.Update(landmarkType);
            _dbContext.SaveChanges();

        }
        public Tuple<List<DictionaryCityModel>, int> GetDictionaryCity(int page, int pageSize, int countyId, string cityName)
        {
            int skip = (page - 1) * pageSize;
            var x = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.DictionaryCityId,
                Name = a.DictionaryCityName,
                CountyId = a.DictionaryCounty.DictionaryCountyId,
                CountyName = a.DictionaryCounty.DictionaryCountyName

            });
            if (countyId != 0)
            {
                x = x.Where(p => p.CountyId == countyId);
            }
            if (!string.IsNullOrEmpty(cityName))
            {
                x = x.Where(p => p.Name.Contains(cityName));
            }
            List<DictionaryCityModel> dictionaryCity = x.Skip(skip).Take(pageSize).ToList();
            return Tuple.Create<List<DictionaryCityModel>, int>(dictionaryCity, x.Count());
        }

        public List<DictionaryCountyModel> ServerFiltering_GetCounties(string text)
        {
            var x = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName,
                CountyCode = a.DictionaryCountyCode
            });
            if (!string.IsNullOrEmpty(text))
            {
                x = x.Where(p => p.CountyName.StartsWith(text));
            }
            List<DictionaryCountyModel> dictionaryCountyModels = x.Take(50).ToList();

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
        
        public int GetCityCount()
        {
            return _dbContext.DictionaryCity.Count();
        }
        public int GetLandmarkCount()
        {
            return _dbContext.DictionaryLandmarkType.Count();
        }
        public int GetCountryCount()
        {
            return _dbContext.DictionaryCountry.Count();
        }

        public int GetDictionarySeasonTypeCount()
        {
            return _dbContext.DictionarySeasonType.Count();
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
        public List<DictionaryCountyModel> GetDictionaryCountyModels(int page, int pageSize,
            int? countryId, string countyName, string countyCode, out int countiesCount)
        {
            int skip = (page - 1) * pageSize;

            var x = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName,
                CountyCode = a.DictionaryCountyCode,
                CountryId = a.DictionaryCountry.DictionaryCountryId,
                CountryName = a.DictionaryCountry.DictionaryCountryName
            });

            if (countryId.HasValue)
            {
                x = x.Where(p => p.CountryId == countryId);
            }

            if (!string.IsNullOrEmpty(countyName))
            {
                x = x.Where(p => p.CountyName.StartsWith(countyName));
            }

            if (!string.IsNullOrEmpty(countyCode))
            {
                x = x.Where(p => p.CountyCode.StartsWith(countyCode));
            }

            countiesCount = x.Count();

            List<DictionaryCountyModel> dictionaryCountyModels = x.Skip(skip).Take(pageSize).ToList();

            return dictionaryCountyModels;
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

        public int DeleteCounty(int? countyId)
        {
            int status;
            try
            {
                DictionaryCounty countyToDelete = new DictionaryCounty();
                countyToDelete = _dbContext.DictionaryCounty.Where(a => a.DictionaryCountyId == countyId).FirstOrDefault();

                if (countyId.HasValue)
                {
                    _dbContext.DictionaryCounty.Remove(countyToDelete);
                }
                status = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                status = 500;
            }

            return status;
        }
        public DictionaryCountyModel getInfoCounty(int countyId)
        {
            DictionaryCountyModel countyToEdit = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.DictionaryCountyId,
                CountyName = a.DictionaryCountyName,
                CountyCode = a.DictionaryCountyCode,
                CountryId = a.DictionaryCountry.DictionaryCountryId,
                CountryName = a.DictionaryCountry.DictionaryCountryName
            })
            .Where(a => a.CountyId == countyId)
            .FirstOrDefault();
            return countyToEdit;
        }

        public int AddCounty(string countyName, string countyCode, int countryId)
        {
            int status;
            try
            {
                _dbContext.DictionaryCounty.Add(new DictionaryCounty
                {
                    DictionaryCountyName = countyName,
                    DictionaryCountyCode = countyCode,
                    DictionaryCountryId = countryId
                });
                status = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                status = 500;
            }
            return status;
        }

        public int EditCounty(int countyId, string countyName, string countyCode, int countryId)
        {
            int status;
            DictionaryCounty countyToEdit = new DictionaryCounty()
            {
                DictionaryCountyId = countyId,
                DictionaryCountyName = countyName,
                DictionaryCountyCode = countyCode,
                DictionaryCountryId = countryId
            };
            try
            {
                _dbContext.DictionaryCounty.Update(countyToEdit);
                _dbContext.SaveChanges();
                status = 1;
            }
            catch (Exception e)
            {
                status = 500;
            }
            return status;
        }
        public void DeleteLandmark(int? id)
        {

            var landmark = _dbContext.DictionaryLandmarkType.Find(id);

            if (id.HasValue)
            {
                _dbContext.DictionaryLandmarkType.Remove(landmark);
            }
            _dbContext.SaveChanges();
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
        public List<DictionaryLandmarkType> FilterLandmarksByName(int page, int pageSize, string LandmarkName)//filtrare dupa nume
        {
            List<DictionaryLandmarkType> filterDictionaryLandmarkTypes = _dbContext.DictionaryLandmarkType
                .Where(a => a.DictionaryItemName == LandmarkName || a.DictionaryItemName.StartsWith(LandmarkName))
                .Select(a => new DictionaryLandmarkType()
                {
                    DictionaryItemId = a.DictionaryItemId,
                    DictionaryItemCode = a.DictionaryItemCode,
                    DictionaryItemName = a.DictionaryItemName,
                }).ToList();

            return filterDictionaryLandmarkTypes;
        }
        public List<DictionaryLandmarkType> FilterLandmarksByCode(int page, int pageSize, string LandmarkCode)//filtrare dupa cod
        {
            List<DictionaryLandmarkType> filterDictionaryLandmarkTypes = _dbContext.DictionaryLandmarkType
                .Where(a => a.DictionaryItemCode == LandmarkCode || a.DictionaryItemCode.StartsWith(LandmarkCode))
                .Select(a => new DictionaryLandmarkType()
                {
                    DictionaryItemId = a.DictionaryItemId,
                    DictionaryItemCode = a.DictionaryItemCode,
                    DictionaryItemName = a.DictionaryItemName,
                }).ToList();

            return filterDictionaryLandmarkTypes;
        }
        public List<DictionaryLandmarkType> FilterLandmarksByCodeAndName(int page, int pageSize, string LandmarkName, string LandmarkCode)//filtrare dupa nume si cod
        {
            List<DictionaryLandmarkType> filterDictionaryLandmarkModels = _dbContext.DictionaryLandmarkType
                .Where(a => a.DictionaryItemName == LandmarkName || a.DictionaryItemName.StartsWith(LandmarkName))
                .Where(a => a.DictionaryItemCode == LandmarkCode || a.DictionaryItemCode.StartsWith(LandmarkCode))
                .Select(a => new DictionaryLandmarkType()
                {
                    DictionaryItemId = a.DictionaryItemId,
                    DictionaryItemCode = a.DictionaryItemCode,
                    DictionaryItemName = a.DictionaryItemName,
                }).ToList();

            return filterDictionaryLandmarkModels;
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

        public void DeleteCountry(int CountryId)
        {
            using (_dbContext)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var Country = from tb in _dbContext.DictionaryCountry
                                      where tb.DictionaryCountryId == CountryId
                                      select tb;
                        if (Country != null)
                        {

                            foreach (var item in Country)
                                _dbContext.DictionaryCountry.Remove(item);

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

        public DictionaryCountry GetSelectedCountry(int CountryId)
        {
            if (CountryId == 0)
            {
                return null;
            }
            List<DictionaryCountry> dictionaryCountry = _dbContext.DictionaryCountry
                .Where(a => a.DictionaryCountryId == CountryId).Select(a => new DictionaryCountry()
                {
                    DictionaryCountryId = a.DictionaryCountryId,
                    DictionaryCountryCode = a.DictionaryCountryCode,
                    DictionaryCountryName = a.DictionaryCountryName

                }).ToList();
            return dictionaryCountry[0];
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
        public void DeleteSeason(int SeasonId)
        {
            using (_dbContext)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
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

        public List<DictionarySeasonType> GetDictionarySeasonTypeModelsbyId(int Page, int PageSize, int DictionarySeasonId)
        {
            int skip = (Page - 1) * PageSize;
            List<DictionarySeasonType> dictionarySeasonTypeModels = _dbContext.DictionarySeasonType.Select(a => new DictionarySeasonType()
            {
                DictionarySeasonId = a.DictionarySeasonId,
                DictionarySeasonCode = a.DictionarySeasonCode,
                DictionarySeasonName = a.DictionarySeasonName

            }).Where(a => a.DictionarySeasonId == DictionarySeasonId).Skip(skip).Take(PageSize).ToList();
            return dictionarySeasonTypeModels;

        }

        public int InsertCity(DictionaryCity city)
        {
            int status;
            try
            {
                _dbContext.DictionaryCity.Add(city);
                status = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                status = 500;
            }
            return status;
        }
        public int UpdateCity(DictionaryCity city)
        {
            int status;
            try
            {
                _dbContext.DictionaryCity.Update(city);
                status = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                status = 500;
            }
            return status;
        }

        public void DeleteCity(int id)
        {
            DictionaryCity city = new DictionaryCity { DictionaryCityId = id };
            _dbContext.DictionaryCity.Remove(city);
            _dbContext.SaveChanges();
        }

        public DictionaryCityModel GetSelectedCity(int id)
        {
            if (id == 0)
            {
                return null;
            }
            List<DictionaryCityModel> dictionaryCityModels = _dbContext.DictionaryCity.Where(a => a.DictionaryCityId == id).Select(a => new DictionaryCityModel()
            {
                Id = a.DictionaryCityId,
                Name = a.DictionaryCityName,
                Code = a.DictionaryCityCode,
                CountyId = a.DictionaryCounty.DictionaryCountyId,
                CountyName = a.DictionaryCounty.DictionaryCountyName

            }).ToList();
            return dictionaryCityModels[0];
        }
        public DictionaryCityModel GetCityToEdit(int id)
        {
            DictionaryCityModel x = _dbContext.DictionaryCity.Select(c => new DictionaryCityModel()
            {
                Id = c.DictionaryCityId,
                Name = c.DictionaryCityName,
                Code = c.DictionaryCityCode,
                CountyName = _dbContext.DictionaryCounty.Where(d => d.DictionaryCountyId == c.DictionaryCountyId).Select(a => a.DictionaryCountyName).FirstOrDefault().ToString()
            }).Where(a => a.Id == id).FirstOrDefault();

            return x;


        }
        public void UpdateSeason(DictionarySeasonType dictionarySeason)
        {
            _dbContext.DictionarySeasonType.Update(dictionarySeason);
            _dbContext.SaveChanges();

        }
        public void InsertSeason(DictionarySeasonType dictionarySeason)
        {
            _dbContext.DictionarySeasonType.Add(dictionarySeason);
            _dbContext.SaveChanges();

        }
        public DictionarySeasonType GetSelectedSeason(int SeasonId)
        {
            if (SeasonId == 0)
            {
                return null;
            }
            List<DictionarySeasonType> dictionarySeason = _dbContext.DictionarySeasonType
                .Where(a => a.DictionarySeasonId == SeasonId).Select(a => new DictionarySeasonType()

                {
                    DictionarySeasonId = a.DictionarySeasonId,
                    DictionarySeasonCode = a.DictionarySeasonCode,
                    DictionarySeasonName = a.DictionarySeasonName


                }).ToList();
            return dictionarySeason[0];
        }


        // ******************   23.09 - Modificari Dragos - Start  ******************************
        public void UpdateCountry(DictionaryCountry dictionaryCountry)
        {
            _dbContext.DictionaryCountry.Update(dictionaryCountry);
            _dbContext.SaveChanges();
        }

        public void InsertCountry(DictionaryCountry dictionaryCountry)
        {
            _dbContext.DictionaryCountry.Add(dictionaryCountry);
            _dbContext.SaveChanges();
        }
        // ******************   23.09 - Modificari Dragos - End  ******************************

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
        public int DeleteCurrency(int id)
        {
            int status = 0;
            DictionaryCurrencyType getDictionaryId = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyType()
            {
                DictionaryCurrencyTypeId = a.DictionaryCurrencyTypeId,
            }).Where(a => a.DictionaryCurrencyTypeId.Equals(id)).FirstOrDefault();
            try
            {
                DictionaryCurrencyType dictionary = new DictionaryCurrencyType() { DictionaryCurrencyTypeId = getDictionaryId.DictionaryCurrencyTypeId };
                _dbContext.DictionaryCurrencyType.Remove(dictionary);
                status =_dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                status = 500;
            }
            return status;
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
