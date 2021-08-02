using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.ScholarshipPage
{
    public class IndexModel : PageModel
    {
        private readonly StudentDataAppContext _context;

        public IndexModel(StudentDataAppContext context)
        {
            _context = context;
        }

        public IList<Scholarship> Scholarship { get;set; }
        public List<SelectListItem> AcceptStatusList = ScholarshipStatusSelectList.getItems();
        public List<SelectListItem> AwardStatusList = ScholarshipStatusSelectList.getItems();

        public string InputToDate { get; set; }
        public string InputFromDate { get; set; }

        public double TotalAmountAwarded { get; set; }

        public async Task OnGetAsync(string fromDate = null, string toDate = null, string acceptStatus = "IGNORE", string awardStatus = "IGNORE")
        {
            InputFromDate = fromDate;
            InputToDate = toDate;
            AcceptStatusList.Find(item => item.Value == acceptStatus).Selected = true;
            AwardStatusList.Find(item => item.Value == awardStatus).Selected = true;
            //Assuming querying all
            if (fromDate == null || toDate == null)
            {
                /* We knew the world would not be the same. A few people laughed, a few people cried, 
                 * most people were silent. I remembered the line from the Hindu scripture, the Bhagavad-Gita.
                 * Vishnu is trying to persuade the Prince that he should do his duty and to impress him 
                 * takes on his multi-armed form and says, 
                 * “Now, I am become Death, the destroyer of worlds.”
                 */
                Scholarship = await _context.Scholarship.Where(s => (
                    (awardStatus == "IGNORE" || (awardStatus == "TRUE" ? s.DateAwarded != null : s.DateAwarded == null)) &&
                    (acceptStatus == "IGNORE" || (acceptStatus == "TRUE" ? s.DateAccepted != null : s.DateAccepted == null))
                    )
                ).Include(i => i.Student).ToListAsync();
            } else
            {
                Scholarship = await _context.Scholarship.Where(s => (
                    s.DateAwarded >= DateTime.Parse(fromDate) && s.DateAwarded <= DateTime.Parse(toDate) &&
                    (awardStatus == "IGNORE" || (awardStatus == "TRUE" ? s.DateAwarded != null : s.DateAwarded == null)) &&
                    (acceptStatus == "IGNORE" || (acceptStatus == "TRUE" ? s.DateAccepted != null : s.DateAccepted == null))
                    )
                ).Include(i => i.Student).ToListAsync();
            }

            TotalAmountAwarded = 0;
            foreach (Scholarship s in Scholarship) {
                TotalAmountAwarded += s.ScholarshipAmount;
            }
        }

        public void OnPostAsync(int? id)
        {
            if (id != null) {
                Response.Redirect("ContactInfoPage/Details/" + id.ToString(), false);
            }
        }
    }
}
