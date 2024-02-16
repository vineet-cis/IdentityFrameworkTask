using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IdentityFrameworkTask.Helpers
{
    public class TypeEnum
    {
        public static List<SelectListItem> PriorityStatusDDL()
        {
            var ddl = new List<SelectListItem>()
          {
               new SelectListItem() { Value = "0", Text = "Select Type"},
               new SelectListItem() { Value = "1", Text = "Dance"},
               new SelectListItem() { Value = "2", Text = "Singing"},
               new SelectListItem() { Value = "3", Text = "Drama"},
               new SelectListItem() { Value = "4", Text = "Sports"},
               new SelectListItem() { Value = "5", Text = "Debate"}

         };
            return ddl;
        }

    }
}
