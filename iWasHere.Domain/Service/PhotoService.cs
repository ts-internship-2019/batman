using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iWasHere.Domain.Service
{
    public class PhotoService
    {
        private readonly DatabaseContext _dbContext;

        public PhotoService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<AttractionsModel> GetAttractions()
        {
            List<AttractionsModel> attractions = _dbContext.Attractions.Select(a => new AttractionsModel()
            {
                AttractionId = a.AttractionId,
                AttractionName = a.AttractionName
            }).ToList();

            return attractions;
        }

        public void SaveImagesDB(string path, int id)
        {
            Photo photo = new Photo()
            {
                PhotoName = path,
                AttractionId = id
            };
            _dbContext.Photo.Add(photo);
            _dbContext.SaveChanges();
        }
    }
}
