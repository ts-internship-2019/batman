using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class AttractionListModel
    {
        public int AttractionId { get; set; }
        public string Observations { get; set; }
        public string CurrencyName { get; set; }
        public decimal Price { get; set; }
        public string LandmarkTypeName { get; set; }
        public string AttractionTypeName { get; set; }
        public string SeasonName { get; set; }
        public string Name { get; set; }
        public string MainPhotoName { get; set; }
        public double? Rating { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public DictionaryCounty County { get; set; }
        public DictionaryCity City { get; set; }
    }
}
