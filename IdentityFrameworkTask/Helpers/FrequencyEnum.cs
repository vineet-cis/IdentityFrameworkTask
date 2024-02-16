using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IdentityFrameworkTask.Helpers
{
    public class FrequencyEnum
    {
        public static List<SelectListItem> FrequencyDDL()
        {
            var ddl = new List<SelectListItem>()
          {
               new SelectListItem() { Value = "0", Text = " --select-- "},
               new SelectListItem() { Value = "1", Text = "OneDay"},
               new SelectListItem() { Value = "2", Text = "Weekly"},
               new SelectListItem() { Value = "3", Text = "Monthly"}
         };
            return ddl;
        }
    }
}
