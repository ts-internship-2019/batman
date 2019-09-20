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
           
            
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).Skip(skip).Take(pageSize).ToList();


            return dictionaryLandmarkTypeModels;
        }

        public Tuple<List<DictionaryCityModel>, int> GetDictionaryCity(int page, int pageSize, int? countyId, string cityName )
        {
            int skip = (page-1) * pageSize;

            
            var x = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.DictionaryCityId,
                Name = a.DictionaryCityName,
                CountyId = a.DictionaryCounty.DictionaryCountyId,
                CountyName = a.DictionaryCounty.DictionaryCountyName

            });
            if (countyId!=0)
            {
                x = x.Where(p => p.CountyId == countyId);
            }
            if (!string.IsNullOrEmpty(cityName))
            {
                x = x.Where(p => p.Name.Contains(cityName));
             }
            List<DictionaryCityModel> dictionaryCity = x.Skip(skip).Take(pageSize).ToList();
            return Tuple.Create< List<DictionaryCityModel>, int>(dictionaryCity, x.Count());
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

        public int GetCityCount()
        {
            return _dbContext.DictionaryCity.Count();
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

        public int InsertCity(DictionaryCity city)
        {
            int status;
            try
            {
                _dbContext.DictionaryCity.Add(city);
                status = _dbContext.SaveChanges();
            }
            catch(Exception e)
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
    }
}
