using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryAttractionType
    {
        public DictionaryAttractionType()
        {
            Attractions = new HashSet<Attractions>();
        }

        public int DictionaryAttractionTypeId { get; set; }
        public string DictionaryAttractionCode { get; set; }
        public string DictionaryAttractionName { get; set; }

        public virtual ICollection<Attractions> Attractions { get; set; }
    }
}
