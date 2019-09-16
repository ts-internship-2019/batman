using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryCity
    {
        public DictionaryCity()
        {
            Attractions = new HashSet<Attractions>();
        }

        public int DictionaryCityId { get; set; }
        public string DictionaryCityName { get; set; }
        public string DictionaryCityCode { get; set; }
        public int? DictionaryCountyId { get; set; }

        public virtual DictionaryCounty DictionaryCounty { get; set; }
        public virtual ICollection<Attractions> Attractions { get; set; }
    }
}
