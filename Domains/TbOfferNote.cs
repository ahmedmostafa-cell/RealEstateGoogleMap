using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domains
{
    public partial class TbOfferNote
    {
        public Guid? OfferNotesId { get; set; }
        public Guid? OfferId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderDepartment { get; set; }
        public string RecieverId { get; set; }
        public string RecieverName { get; set; }
        public string RecieverDepartment { get; set; }
        [Required(ErrorMessage = "من فضلك ادخل  الملاحظة   ")]
        public string NoteSyntax { get; set; }
        public string Visibility { get; set; }
        public string ReadByAdmin { get; set; }
        public string ReadByReceptionEmployee { get; set; }
        public string ReadBySalesEmployee { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
