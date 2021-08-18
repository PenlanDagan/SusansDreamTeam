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

        public string InputStartTerm { get; set; }

        public double TotalAmountAwarded { get; set; }

        public async Task OnGetAsync(string startTerm = null, string acceptStatus = null, string awardStatus = null)
        {
            if (startTerm == null && acceptStatus == null && awardStatus == null)
            {
                return;
            }

            InputStartTerm = startTerm;
            AcceptStatusList.Find(item => item.Value == acceptStatus).Selected = true;
            AwardStatusList.Find(item => item.Value == awardStatus).Selected = true;
            //Assuming querying all
            if (startTerm == null || startTerm.Trim().Length == 0)
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
                    s.Student.Post_Registrations.Any(p => p.StartTerm == startTerm.Trim()) &&
                    (awardStatus == "IGNORE" || (awardStatus == "TRUE" ? s.DateAwarded != null : s.DateAwarded == null)) &&
                    (acceptStatus == "IGNORE" || (acceptStatus == "TRUE" ? s.DateAccepted != null : s.DateAccepted == null))
                    )
                ).Include(scholarship => scholarship.Student).ThenInclude(student => student.Post_Registrations).ToListAsync();
            }

            TotalAmountAwarded = 0;
            foreach (Scholarship s in Scholarship) {
                TotalAmountAwarded += s.ScholarshipAmount;
            }
        }
    }
}
