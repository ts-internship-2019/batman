using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class Comment
    {
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public int CommentId { get; set; }
        public int AttractionId { get; set; }
        public string UserId { get; set; }

        public virtual Attractions Attraction { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
