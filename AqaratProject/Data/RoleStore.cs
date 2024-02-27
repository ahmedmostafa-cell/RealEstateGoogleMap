using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AqaratProject.Data
{
    public static class RoleStore
    {
        public static List<Claim> claimsList = new List<Claim>()
        {
            new Claim("Admin","Admin"),
            new Claim("User","User"),
             new Claim("Watcher","Watcher"),

        };
    }
}
