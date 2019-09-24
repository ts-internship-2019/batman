using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCurrencyTypeModel
    {
        public int DicurrencyId { get; set; }

        [Required(ErrorMessage = "Currency Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Currency Code is required.")]
        public string Code { get; set; }
        public int Id { get; set; }
        public int CurrencyId { get; set; }

        public int TypeId { get; set; }

        public int CurrencyTypeId { get; set; }


        [Required(ErrorMessage = "Currency Value is required.")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Currency Date is required.")]
        public DateTime Data { get; set; }



    }
}
