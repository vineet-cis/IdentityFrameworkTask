using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityFrameworkTask.DataModel
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}