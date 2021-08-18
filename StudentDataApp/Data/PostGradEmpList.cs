using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentDataApp.Data
{
    public class PostGradEmpList
    {
        private static List<SelectListItem> empSelectList;

        public static List<SelectListItem> getItems()
        {

            empSelectList = new List<SelectListItem>();

            empSelectList.Add(new SelectListItem { Value = "Mainframe", Text = "Mainframe" });
            empSelectList.Add(new SelectListItem { Value = "Web Dev", Text = "Web Dev" });
            empSelectList.Add(new SelectListItem { Value = "Mobile", Text = "Mobile" });
            

            return empSelectList;
        }
    }
}
