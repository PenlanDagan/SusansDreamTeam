using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentDataApp.Data
{
    public class PostGradCodeSelectList
    {

        private static List<SelectListItem> gradCodeSelectList;

        public static List<SelectListItem> getItems()
        {

            gradCodeSelectList = new List<SelectListItem>();
            
            gradCodeSelectList.Add(new SelectListItem { Value = "Job", Text = "Job"});
            gradCodeSelectList.Add(new SelectListItem { Value = "Education", Text = "Education" });
            gradCodeSelectList.Add(new SelectListItem { Value = "Military", Text = "Military" });
            gradCodeSelectList.Add(new SelectListItem { Value = "Other", Text = "Other" });


            return gradCodeSelectList;
        }
    }
}

