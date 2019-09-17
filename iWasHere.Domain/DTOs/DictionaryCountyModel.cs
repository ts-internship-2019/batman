using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
   public class DictionaryCountyModel
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public string CountyCode { get; set; }        
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
