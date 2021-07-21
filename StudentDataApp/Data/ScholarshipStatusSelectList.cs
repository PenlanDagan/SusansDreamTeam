using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataApp.Data
{
    public class ScholarshipStatusSelectList
    {
        private static List<SelectListItem> statusSelectList;

        public static List<SelectListItem> getItems()
        {
            statusSelectList = new List<SelectListItem>();
            statusSelectList.Add(new SelectListItem { Value = "IGNORE", Text = "IGNORE" });
            statusSelectList.Add(new SelectListItem { Value = "TRUE", Text = "TRUE" });
            statusSelectList.Add(new SelectListItem { Value = "FALSE", Text = "FALSE" });

            return statusSelectList;
        }
    }
}
