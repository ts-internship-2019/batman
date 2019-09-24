using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryCounty
    {
        public DictionaryCounty()
        {
            DictionaryCity = new HashSet<DictionaryCity>();
        }

        public int DictionaryCountyId { get; set; }
        public string DictionaryCountyName { get; set; }
        public string DictionaryCountyCode { get; set; }
        public int? DictionaryCountryId { get; set; }

        public virtual DictionaryCountry DictionaryCountry { get; set; }
        public virtual ICollection<DictionaryCity> DictionaryCity { get; set; }
    }
}
