using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class Currrency
    {
        public int CurrencyId { get; set; }
        public int? CurrencyTypeId { get; set; }
        public double Value { get; set; }
        public DateTime CurrencyDate { get; set; }

        public virtual DictionaryCurrencyType CurrencyType { get; set; }
    }
}
