using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryLandmarkType
    {
        public DictionaryLandmarkType()
        {
            Attractions = new HashSet<Attractions>();
        }

        public int DictionaryItemId { get; set; }
        public string DictionaryItemCode { get; set; }
        public string DictionaryItemName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Attractions> Attractions { get; set; }
    }
}
