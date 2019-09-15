using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tax_Calculator.Models
{
    public class Helpers
    {
        public decimal Annual_salary { get; set; }
        public string Postal_code { get; set; }
        public decimal Tax_result { get; set; }
        public string Tax_result_message { get; set; }
        public string Error_message { get; set; }
    }
}