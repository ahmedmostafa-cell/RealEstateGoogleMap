using System;
using System.Collections.Generic;

#nullable disable

namespace Domains
{
    public partial class TbRequest
    {
        public Guid? RequestId { get; set; }
        public string RequesterName { get; set; }
        public string RequesterNumber { get; set; }
        public Guid? UnitId { get; set; }
        public string UnitName { get; set; }
        public string RequestText { get; set; }
        public string CashOrCredit { get; set; }
        public string ValueOfPurchase { get; set; }
        public Guid? RegionId { get; set; }
        public string RegionName { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitute { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }


        public string? customer_name { get; set; }

        public decimal? lat { get; set; }

        public decimal? lng { get; set; }
        public string? contract_type { get; set; }
        public string? contract_number { get; set; }
        public string? icon { get; set; }
    }
}
