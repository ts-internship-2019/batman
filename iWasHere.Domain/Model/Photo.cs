using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class Photo
    {
        public int? AttractionId { get; set; }
        public string PhotoName { get; set; }
        public int PhotoId { get; set; }

        public virtual Attractions Attraction { get; set; }
    }
}
