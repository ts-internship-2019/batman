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
            return _dbContext.DictionaryLandmarkType.Count();
        }
        public int GetCountryCount()
        {
            return _dbContext.DictionaryCountry.Count();
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
            //  if (countyId.HasValue)
            // {
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
            //  } else
            //  {
            //     countyId = 0;
            //      return null;
            //    }           


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
                        var County = from tb in _dbContext.DictionaryCounty
                                     where tb.DictionaryCountryId == CountryId
                                     select tb;

                        if (County != null)
                        {
                            foreach (var item in County)
                            {
                                var City = from tb in _dbContext.DictionaryCity
                                           where tb.DictionaryCountyId == item.DictionaryCountyId
                                           select tb;

                                if (City != null)
                                {
                                    foreach (var itemCity in City)
                                        _dbContext.DictionaryCity.Remove(itemCity);
                                }

                                _dbContext.DictionaryCounty.Remove(item);
                            }
                        }

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
    }
}