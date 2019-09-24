using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class Attractions
    {
        public Attractions()
        {
            Comment = new HashSet<Comment>();
            Photo = new HashSet<Photo>();
        }

        public string Observations { get; set; }
        public int CurrencyId { get; set; }
        public int CityId { get; set; }
        public int AttractionId { get; set; }
        public string AttractionName { get; set; }
        public decimal Price { get; set; }
        public int LandmarkTypeId { get; set; }
        public int AttractionTypeId { get; set; }
        public int SeasonId { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public virtual DictionaryAttractionType AttractionType { get; set; }
        public virtual DictionaryCity City { get; set; }
        public virtual DictionaryCurrencyType Currency { get; set; }
        public virtual DictionaryLandmarkType LandmarkType { get; set; }
        public virtual DictionarySeasonType Season { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
    }
}
