using System;
using System.Collections.Generic;

#nullable disable

namespace Domains
{
    public partial class TbOfferVideo
    {
        public Guid? OfferVideoId { get; set; }
        public Guid? OfferId { get; set; }
        public string OfferVideoPath { get; set; }
        public string OfferVideoYouTubeLink { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Notes { get; set; }
        public int? CurrentState { get; set; }
    }
}
