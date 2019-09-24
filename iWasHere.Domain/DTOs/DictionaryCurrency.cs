using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCurrency
    {
        public int CurrencyId { get; set; }

        public int TypeId { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }
        

    }
}
