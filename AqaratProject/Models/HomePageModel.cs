using BL;
using Domains;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AqaratProject.Models
{
    public class HomePageModel
    {
        #region Declaration


        public IEnumerable<ApplicationUser> UserData { get; set; }

        public string ImageProfile { get; set; }

        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

      

        public ApplicationUser OneUser { get; set; }


        public IEnumerable<ApplicationUser> lstUsers { get; set; }


        public IEnumerable<TbInquiry> lstInquiries { get; set; }



        public IEnumerable<TbLogHistory> lstLogHistories { get; set; }

        public IEnumerable<TbOffer> lstOffers { get; set; }

        public IEnumerable<TbOfferImage> lstOfferImages { get; set; }

        public IEnumerable<TbOfferBooking> lsOfferBooking { get; set; }

        public IEnumerable<TbOfferNote> lsOfferNote { get; set; }

        public IEnumerable<TbOfferVideo> lsOfferVideo { get; set; }

        public IEnumerable<TbRegion> lsRegion { get; set; }

        public IEnumerable<TbRequest> lsRequest { get; set; }

        public IEnumerable<TbRequestNote> lsRequestNote { get; set; }

        public IEnumerable<TbSalesRepPoint> lsSalesRepPoint { get; set; }

        public IEnumerable<TbSetting> lsSetting { get; set; }

        public IEnumerable<TbUnit> lsUnit { get; set; }

        public IEnumerable<TbActiveUsers> lsActiveUsers { get; set; }

        public TbOffer OneOffer { get; set; }

        public TbRequest OneRequest { get; set; }
















        #endregion
    }
}
