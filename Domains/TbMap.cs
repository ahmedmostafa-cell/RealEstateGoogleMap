using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public class TbMap
    {
        public int? id { get; set; }
      
        public string? customer_name { get; set; }
       
        public decimal? lat { get; set; }
       
        public decimal? lng { get; set; }
        public string? contract_type { get; set; }
        public int? contract_number { get; set; }
        public string? icon { get; set; }
     
    }
}
