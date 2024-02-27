using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbSalesRepPoint
    {
        public Guid? SalesRepPointId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عدد اسم موظف البيع   ")]
        public Guid? SalesRepId { get; set; }
        public string SalesRepName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل سبب منح النقاط   ")]
        public string Cause { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عدد النقاط   ")]
        public string Points { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
