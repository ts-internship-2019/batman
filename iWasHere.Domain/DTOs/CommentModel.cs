using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class CommentModel
    {
        public string UserID { get; set; }

        public string titlu { get; set; }

        public string descriere { get; set; }

        public int attractionid { get; set; }
        public int rating { get; set; }
        public string numeuser { get; set; }
    }
}
