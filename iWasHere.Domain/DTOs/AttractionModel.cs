﻿using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class AttractionModel
    {
        public int AttractionId { get; set; }
        public int CurrencyId { get; set; }
        public int? CityId { get; set; }
        public decimal Price { get; set; }
        public int? LandmarkTypeId { get; set; }
        public int AttractionTypeId { get; set; }
        public int SeasonId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string AttractionName { get; set; }
        public string Observations { get; set; }
        public string CityName { get; set; }
        public string AttractionTypeName { get; set; }
        public string LandmarkTypeName { get; set; }
        public string SeasonName { get; set; }
        public string CountryName { get; set; }
        public string CountyName { get; set; }

        public int CountryId { get; set; }
        public DictionaryAttractionType AttractionType { get; set; }
        public DictionaryCity City { get; set; }
        public DictionaryCurrencyType Currency { get; set; }
        public DictionaryLandmarkType LandmarkType { get; set; }
        public DictionarySeasonType Season { get; set; }
        public List<Comment> Comment { get; set; }
        public List<string> PhotoName { get; set; }
        public List<Photo> Photo { get; set; }

        public DictionaryCounty County { get; set; }
        public DictionaryCountry Country { get; set; }
    }
}
