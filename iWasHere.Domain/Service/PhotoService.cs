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

        public int AddPhoto(int attractionId, string photoName, string photoPath)
        {
            int status = 0;
            Photo image = new Photo()
            {
                AttractionId = attractionId,
                PhotoName = photoName,
                Path = photoPath
            };
            try
            {
                 _dbContext.Photo.Add(image);
                status =_dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                status = 500;
            }
            return status;
        }
    }
}
