using System;
using System.Collections.Generic;
using System.Text;

namespace Domains
{
    public partial class TbRealTimeNotifcation
    {
        public Guid RealTimeNotifcationId { get; set; }
        public string? NotificationType { get; set; }
        public string? NotificationSyntax { get; set; }

       
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Notes { get; set; }
    }
}
