using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbInquiry
    {
        public Guid? InquiryId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اسم صاحب الاستفسار   ")]
        public string InquiryOwnerName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل   الاستفسار   ")]
        public string InquirySyntax { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل  رقم تليفون صاحب الاستفسار   ")]
        public string InquiryOwnerPhoneNumber { get; set; }
        public string ReceptionRepId { get; set; }
        public string ReceptionRepName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
