using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public partial class TbSetting
    {
        public Guid? SettingId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل عدد ايام  حجز الوحدة   ")]
        public string? NoOfBookingDays { get; set; }
      
      
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
