using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionarySeasonType
    {
        public DictionarySeasonType()
        {
            Attractions = new HashSet<Attractions>();
        }

        public string DictionarySeasonName { get; set; }
        public string DictionarySeasonCode { get; set; }
        public int DictionarySeasonId { get; set; }

        public virtual ICollection<Attractions> Attractions { get; set; }
    }
}
