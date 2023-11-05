using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Models;

namespace Q2.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly PRN_Spr23_B1Context _context;

        public IndexModel(PRN_Spr23_B1Context context)
        {
            _context = context;
        }

        public List<Employee> Employees { get; set; }

        public void OnGet()
        {
            Employees = _context.Employees.ToList();
        }
    }
}
