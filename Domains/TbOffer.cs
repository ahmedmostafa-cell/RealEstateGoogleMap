using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbOffer
    {
        public Guid? OfferId { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل صيغة العرض   ")]
        public string OfferSyntax { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل اسم صاحب العرض   ")]
        public string OwnerName { get; set; }
       
        public string OwnerNameField { get; set; }
        [Required(ErrorMessage = "من فضلك رقم تليفون اسم صاحب العرض   ")]
        public string OwnerPhoneNumber { get; set; }
       
        public string OwnerPhoneNumberField { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل نوع الوحدة العقارية   للعرض   ")]
        public Guid? UnitId { get; set; }
        public string UnitName { get; set; }
       
        public string UnitNameField { get; set; }
       
        public string PropertyDocumentPath { get; set; }
       
        public string PropertyDocumentPathField { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل  موقع الوحدة العقارية   ")]
        public string Location { get; set; }
       
        public string LocationField { get; set; }
       
        public string Latitude { get; set; }
       
        public string LatitudeField { get; set; }
       
        public string Longitute { get; set; }
       
        public string LongituteField { get; set; }
        public string SalesRepId { get; set; }
        public string SalesRepName { get; set; }
        public string SalesRepNameField { get; set; }
        public string ReceptionRepId { get; set; }
      
        public string ReceptionRepName { get; set; }
       
        public string ReceptionRepNameField { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل  اسم منطقة الوحدة العقارية   ")]
        public Guid? RegionId { get; set; }
        public string RegionName { get; set; }
        
        public string RegionNameField { get; set; }
        public string OfferStatus { get; set; }
      
        public string OfferStatusField { get; set; }
      
        public string SellingStatus { get; set; }
       
        public string SellingStatusField { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل  السوم   ")]
        public string OfferStartTime { get; set; }
        
        public string OfferStartTimeFiled { get; set; }
       
        public string OfferEndTime { get; set; }
     
        public string OfferEndTimeField { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }



        public string? customer_name { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل  خطوط عرض الوحدة العقارية   ")]
        public decimal? lat { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل  خطوط طول الوحدة العقارية   ")]
        public decimal? lng { get; set; }
        public string? contract_type { get; set; }
        public string? contract_number { get; set; }
        public string? icon { get; set; }


        public string PurchaserName { get; set; }



        public string PurchaserPhoneNumber { get; set; }



        public string PurchasingValue { get; set; }


        public string PaymentWay { get; set; }


        public string ArchiveReasone { get; set; }
    }
}
