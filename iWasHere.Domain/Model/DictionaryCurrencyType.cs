﻿using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryCurrencyType
    {
        public DictionaryCurrencyType()
        {
            
            Currency = new HashSet<Currency>();
        }

        public string DictionaryCurrencyName { get; set; }
        public string DictionaryCurrencyCode { get; set; }
        public int DictionaryCurrencyTypeId { get; set; }
        //public int CurrencyId { get; set; }

       
        public virtual ICollection<Currency> Currency { get; set; }
    }
}
