using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryCurrencyType
    {
        public DictionaryCurrencyType()
        {
            Attractions = new HashSet<Attractions>();
            Currrency = new HashSet<Currrency>();
        }

        public string DictionaryCurrencyName { get; set; }
        public string DictionaryCurrencyCode { get; set; }
        public int DictionaryCurrencyTypeId { get; set; }
        //public int CurrencyId { get; set; }

        public virtual ICollection<Attractions> Attractions { get; set; }
        public virtual ICollection<Currrency> Currrency { get; set; }
    }
}
