using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCurrencyTypeModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTime Data { get; set; }

    }
}
