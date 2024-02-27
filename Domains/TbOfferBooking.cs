using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbOfferBooking
    {
        public Guid? OfferBookingId { get; set; }
        public Guid? OfferId { get; set; }
        public string SalesRepId { get; set; }
        public string SalesRepName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اسم العميل   ")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل رقم تليفون العميل   ")]
        public string CustomerPhoneNumber { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل القيمة المتوقعة   ")]
        public string ValueToBePaid { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل هل كاش ام اجل   ")]
        public string CashOrCredit { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public string SellingStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
