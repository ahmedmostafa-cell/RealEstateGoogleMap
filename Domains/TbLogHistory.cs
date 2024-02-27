using System;
using System.Collections.Generic;

#nullable disable

namespace Domains
{
    public partial class TbLogHistory
    {
        public Guid? LogHistoryId { get; set; }
        public Guid? LoggedUserId { get; set; }
        public string LoggedUserName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
