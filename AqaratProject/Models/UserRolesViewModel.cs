using System.Collections.Generic;

namespace AqaratProject.Models
{
    public class UserRolesViewModel
    {
        public UserRolesViewModel()
        {
            Claims = new List<UserRole>();
        }
        public string UserId { get; set; }
        public List<UserRole> Claims { get; set; }
    }
    public class UserRole
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
