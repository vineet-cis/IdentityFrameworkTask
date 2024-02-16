using IdentityFrameworkTask.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IdentityFrameworkTask.ViewModels
{
    public class StudentVM
    {
        public Student StuData { get; set; }
        //public List<SelectListItem> Students { get; set; }
        //public int StudentId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
