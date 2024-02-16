using IdentityFrameworkTask.DataModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace IdentityFrameworkTask.ViewModels
{
    public class UserVM
    {
        public AppUser User { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public List<SelectListItem> AppRoles { get; set; }
        public string Role { get; set; }
        public string AppRoleId { get; set; }
        
    }
}
