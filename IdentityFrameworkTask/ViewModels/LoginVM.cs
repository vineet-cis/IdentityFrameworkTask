using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityFrameworkTask.ViewModels
{
    public class LoginVM
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
