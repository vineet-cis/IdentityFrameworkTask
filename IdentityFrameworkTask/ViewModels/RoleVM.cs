using IdentityFrameworkTask.DataModel;
using System;

namespace IdentityFrameworkTask.ViewModels
{
    public class RoleVM
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int NumberOfUsers { get; set; }
        public AppRole Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
