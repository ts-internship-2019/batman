using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class Attractions
    {
        //public Attractions()
        //{
        //    Comment = new HashSet<Comment>();
        //    Photo = new HashSet<Photo>();
        //}

       
        public int CurrencyId { get; set; }
        public int? CityId { get; set; }
        public int AttractionId { get; set; }
        public decimal Price { get; set; }
        public int? LandmarkTypeId { get; set; }
        public int AttractionTypeId { get; set; }
        public int SeasonId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string AttractionName { get; set; }
        public string Observations { get; set; }
        public virtual DictionaryAttractionType AttractionType { get; set; }
        public virtual DictionaryCity City { get; set; }
        public virtual DictionaryCurrencyType Currency { get; set; }
        public virtual DictionaryLandmarkType LandmarkType { get; set; }
        public virtual DictionarySeasonType Season { get; set; }
        public virtual List<Comment> Comment { get; set; }
        public virtual List<Photo> Photo { get; set; }
    }
}
