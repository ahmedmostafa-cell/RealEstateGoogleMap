using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AqaratProject.Models
{
    public class SignUpModel
    {
        public string Password { get; set; }
        public IFormFile PersonalImage { get; set; }
        public string ImageProfile { get; set; }
        public string Email { get; set; }

        public string UserType { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }

        public string FamilyName { get; set; }


        public string Id { get; set; }


        public string OTP { get; set; }


        public string Latitude { get; set; }
        public string Longitute { get; set; }
        public string Location { get; set; }


        public string PhoneNumber { get; set; }
       






    }
}
